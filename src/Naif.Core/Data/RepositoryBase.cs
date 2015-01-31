﻿//******************************************
//  Copyright (C) 2012-2013 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included License.txt file)        *
//                                         *
// *****************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Naif.Core.Caching;
using Naif.Core.ComponentModel;
using Naif.Core.Contracts;

namespace Naif.Core.Data
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Private Members

        private readonly ICacheProvider _cache;

        #endregion

        #region Constructors

        protected RepositoryBase(ICacheProvider cache)
        {
            Requires.NotNull("cache", cache);

            _cache = cache;
        }

        #endregion

        #region IRepository<T> Implementation

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> items = IsCacheable 
                                    ? _cache.GetCachedObject(CacheKey, GetAllInternal) 
                                    : GetAllInternal();
            return items;
        }

        public T GetById<TProperty>(TProperty id)
        {
            T item = IsCacheable 
                         ? GetAll().SingleOrDefault(t => CompareTo(GetPrimaryKey<TProperty>(t), id) == 0) 
                         : GetByIdInternal(id);

            return item;
        }

        public IEnumerable<T> GetByProperty<TProperty>(string propertyName, TProperty propertyValue)
        {
            IEnumerable<T> items = IsCacheable 
                                    ? GetAll().Where(t => CompareTo(GetPropertyValue<TProperty>(t, propertyName), propertyValue) == 0) 
                                    : GetByPropertyInternal(propertyName, propertyValue);

            return items;
        }

        public void Add(T item)
        {
            AddInternal(item);
            ClearCache();
        }

        public void Delete(T item)
        {
            DeleteInternal(item);
            ClearCache();
        }

        public void Update(T item)
        {
            UpdateInternal(item);
            ClearCache();
        }

        #endregion

        #region Private Methods

        private void ClearCache()
        {
            if (IsCacheable)
            {
                _cache.Remove(CacheKey);
            }
        }

        private TProperty GetPropertyValue<TProperty>(TypeInfo modelType, T item, string propertyName)
        { 
            var property = modelType.DeclaredProperties.SingleOrDefault(p => p.Name == propertyName);

            return (TProperty)property.GetValue(item, null);
        }

        #endregion

        #region Protected Methods

        protected string CacheKey
        {
            get
            {
                return Util.GetCacheKey(typeof(T).GetTypeInfo());
            }
        }

        protected int CompareTo<TProperty>(TProperty first, TProperty second)
        {
            Requires.IsTypeOf<IComparable>("first", first);
            Requires.IsTypeOf<IComparable>("second", second);

            var firstComparable = first as IComparable;
            var secondComparable = second as IComparable;

// ReSharper disable PossibleNullReferenceException
            return firstComparable.CompareTo(secondComparable);
// ReSharper restore PossibleNullReferenceException
        }

        protected TProperty GetPropertyValue<TProperty>(T item, string propertyName)
        {
            return GetPropertyValue<TProperty>(typeof(T).GetTypeInfo(), item, propertyName);
        }

        protected TProperty GetPrimaryKey<TProperty>(T item)
        {
            TypeInfo modelType = typeof(T).GetTypeInfo();

            //Get the primary key
            var primaryKeyName = Util.GetPrimaryKeyName(modelType);

            return GetPropertyValue<TProperty>(modelType, item, primaryKeyName);
        }

        protected bool IsCacheable
        {
            get
            {
                return Util.IsCacheable(typeof(T).GetTypeInfo());
            }
        }

        #endregion

        #region Abstract Methods

        protected abstract void AddInternal(T item);

        protected abstract void DeleteInternal(T item);

        protected abstract IEnumerable<T> GetAllInternal();

        protected abstract T GetByIdInternal<TProperty>(TProperty id);

        protected abstract IEnumerable<T> GetByPropertyInternal<TProperty>(string propertyName, TProperty propertyValue);

        protected abstract void UpdateInternal(T item);

        #endregion
    }
}

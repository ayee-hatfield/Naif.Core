//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System;

namespace Naif.Core.Caching
{
    public static class CacheExt
    {
        public static TObject GetCachedObject<TObject>(this ICacheProvider cache, string cacheKey, Func<TObject> getItem) where TObject : class
        {
            var cachedObject = cache.Get(cacheKey);

            if (cachedObject == null)
            {
                // get object from data source using delegate
                if (getItem != null)
                {
                    cachedObject = getItem();
                }

                cache.Insert(cacheKey, cachedObject);
            }

            // return the object
            if (cachedObject == null)
            {
                return default(TObject);
            }
            return cachedObject as TObject;
        }
    }
}

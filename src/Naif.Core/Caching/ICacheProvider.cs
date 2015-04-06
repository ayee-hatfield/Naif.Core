//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

//
// - Inspired by EasyCache.NET (http://easycache.codeplex.com)
// 

using System;

namespace Naif.Core.Caching
{
    public interface ICacheProvider
    {
        object this[string key] { get; set; }

        object Get(string key);

        void Insert(string key, object value, DateTime absoluteExpiration);
        void Insert(string key, object value);
        void Remove(string key);
    }
}

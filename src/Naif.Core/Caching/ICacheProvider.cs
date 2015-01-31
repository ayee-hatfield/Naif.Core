//******************************************
//  Copyright (C) 2012-2013 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included License.txt file)        *
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

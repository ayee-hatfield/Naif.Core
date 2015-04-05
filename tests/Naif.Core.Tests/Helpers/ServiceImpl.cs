//******************************************
//  Copyright (C) 2012-2013 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included License.txt file)        *
//                                         *
// *****************************************

using System;

namespace Naif.Core.Tests.Helpers
{

    public class ServiceImpl : IService
    {
        private static readonly Random rnd = new Random();
        private readonly int id = rnd.Next();

        public int Id 
        { 
            get { return id; } 
        }
    }
}

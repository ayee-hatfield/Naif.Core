//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System.Collections.Generic;

namespace Naif.Core.Tests
{
    public class Util
    {
        public static List<int> CreateIntegerList(int count)
        {
            var list = new List<int>();
            for (int i = 0; i < count; i++)
            {
                list.Add(i);
            }
            return list;
        }
    }
}

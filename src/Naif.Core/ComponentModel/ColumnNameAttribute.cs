//******************************************
//  Copyright (C) 2012-2013 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included License.txt file)        *
//                                         *
// *****************************************

using System;

namespace Naif.Core.ComponentModel
{
    public class ColumnNameAttribute : Attribute
    {
        public string ColumnName { get; set; }
    }
}

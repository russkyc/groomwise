﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System;
using System.Globalization;
using System.Windows.Data;

namespace GroomWise.Converters;

[ValueConversion(typeof(DateTime),typeof(string))]
public class DateTimeToTimeConverter : IValueConverter
{

    public static DateTimeToTimeConverter Instance = new DateTimeToTimeConverter();
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var date = value is DateTime
            ? (DateTime)value : default;
        return date.ToString("h:mm");;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
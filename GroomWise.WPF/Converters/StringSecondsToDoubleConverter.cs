// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System;
using System.Globalization;
using System.Windows.Data;

namespace GroomWise.Converters;

[ValueConversion(typeof(double), typeof(string))]
public class StringSecondsToDoubleConverter : IValueConverter
{
    public static StringSecondsToDoubleConverter Instance = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double seconds)
        {
            return $"{seconds:1F}s";
        }

        return "0.0s";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string seconds)
        {
            return double.Parse(seconds.Remove(seconds.Length - 1, 1));
        }

        return 0.0;
    }
}

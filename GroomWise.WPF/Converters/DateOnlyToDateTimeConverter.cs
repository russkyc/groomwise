// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System;
using System.Globalization;
using System.Windows.Data;

namespace GroomWise.Converters;

[ValueConversion(typeof(DateOnly), typeof(DateTime))]
public class DateOnlyToDateTimeConverter : IValueConverter
{
    public static DateOnlyToDateTimeConverter Instance = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var dt = (DateOnly)value;
        return new DateTime(dt.Year, dt.Month, dt.Day);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var d = (DateTime)value;
        return DateOnly.FromDateTime(d);
    }
}

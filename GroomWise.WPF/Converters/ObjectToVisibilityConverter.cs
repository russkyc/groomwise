// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GroomWise.Converters;

[ValueConversion(typeof(object), typeof(Visibility))]
public class ObjectToVisibilityConverter : IValueConverter
{
    public static ObjectToVisibilityConverter Instance = new();

    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return Visibility.Collapsed;
        }

        return Visibility.Visible;
    }

    public object ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

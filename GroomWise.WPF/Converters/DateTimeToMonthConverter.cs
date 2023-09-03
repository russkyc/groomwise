// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

using System;
using System.Globalization;
using System.Windows.Data;

namespace GroomWise.Converters;

[ValueConversion(typeof(DateTime), typeof(string))]
public class DateTimeToMonthConverter : IValueConverter
{
    public static DateTimeToMonthConverter Instance = new DateTimeToMonthConverter();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var date = value as DateTime? ?? default;
        return date.ToString("MMMM yyyy");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

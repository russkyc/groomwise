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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using GroomWise.Domain.Enums;

namespace GroomWise.Converters;

[ValueConversion(typeof(Role), typeof(Visibility))]
public class RoleToVisibilityConverter : IValueConverter
{
    public static readonly RoleToVisibilityConverter Instance = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not IEnumerable<Role> roles)
        {
            return Visibility.Collapsed;
        }

        if (value is not Role accessRole)
        {
            return Visibility.Collapsed;
        }

        if (roles.Any(role => role == accessRole))
        {
            return Visibility.Visible;
        }
        
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Converter;

[ValueConversion(typeof(string), typeof(bool))]
public class StringSelectionToBooleanConverter : IValueConverter
{
    public static StringSelectionToBooleanConverter Instance =
        new StringSelectionToBooleanConverter();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (string)value == (string)parameter;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

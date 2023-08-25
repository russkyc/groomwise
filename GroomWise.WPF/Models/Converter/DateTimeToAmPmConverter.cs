// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Converter;

[ValueConversion(typeof(DateTime), typeof(string))]
public class DateTimeToAmPmConverter : IValueConverter
{
    public static DateTimeToAmPmConverter Instance => new DateTimeToAmPmConverter();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var date = value as DateTime? ?? default;
        return date.Hour switch
        {
            <= 11 => "AM",
            > 11 => "PM"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

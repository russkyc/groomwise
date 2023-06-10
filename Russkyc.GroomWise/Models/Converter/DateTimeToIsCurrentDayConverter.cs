// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Converter;

[ValueConversion(typeof(DateTime), typeof(bool))]
public class DateTimeToIsCurrentDayConverter : IValueConverter
{
    public static DateTimeToIsCurrentDayConverter Instance = new DateTimeToIsCurrentDayConverter();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var date = value as DateTime? ?? default;
        return date.Day == DateTime.Today.Day;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

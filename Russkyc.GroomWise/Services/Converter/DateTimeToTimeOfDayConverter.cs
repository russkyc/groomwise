// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Converter;

[ValueConversion(typeof(DateTime), typeof(string))]
public class DateTimeToTimeOfDayConverter : IValueConverter
{
    public static DateTimeToTimeOfDayConverter Instance = new DateTimeToTimeOfDayConverter();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var date = value as DateTime? ?? default;
        return date.Hour switch
        {
            <= 11 => "Morning",
            <= 16 => "Afternoon",
            <= 23 => "Evening",
            _ => "Night"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

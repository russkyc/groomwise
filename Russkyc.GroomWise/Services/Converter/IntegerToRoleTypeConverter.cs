// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Converter;

[ValueConversion(typeof(int), typeof(EmployeeType))]
public class IntegerToRoleTypeConverter : IValueConverter
{
    public static IntegerToRoleTypeConverter Instance => new IntegerToRoleTypeConverter();

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var type = value is int i ? i : 0;
        return type switch
        {
            0 => EmployeeType.Admin,
            1 => EmployeeType.Manager,
            2 => EmployeeType.Groomer,
            _ => null
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

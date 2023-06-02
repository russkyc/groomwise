// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Helper;

public static class DataClassHelper
{
    public static bool HasSameValues(
        this object source,
        object target,
        params string[] propertyNames
    )
    {
        var properties = source.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (propertyNames.Any(prop => property.Name == prop))
            {
                var sourceValue = property.GetValue(source);
                var targetValue = property.GetValue(target);
                if (!Equals(sourceValue, targetValue))
                {
                    return false;
                }
            }
        }

        return true;
    }
}

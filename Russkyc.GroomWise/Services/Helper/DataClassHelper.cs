// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Helper;

public static class DataClassHelper
{
    public static bool HasSameValues(this object source, object target, params string[] except)
    {
        return source
            .GetType()
            .GetProperties()
            .Where(prop => !except.Contains(prop.Name))
            .ToList()
            .All(property => Equals(property.GetValue(source), property.GetValue(target)));
    }
}

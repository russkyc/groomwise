// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Application.Extensions;

public static class NameExtensions
{
    public static string GetFirstName(this string fullName)
    {
        var nameSections = fullName.Split(" ");
        return nameSections[0];
    }
}

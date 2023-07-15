// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Domain.Extensions;

public static class CompareExtensions
{
    public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

    public static bool IsNotNullOrEmpty(this string str) => !IsNullOrEmpty(str);
}

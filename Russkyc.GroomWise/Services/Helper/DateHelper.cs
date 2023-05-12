// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Helper;

public static class DateHelper
{
    public static string GetMonth(this DateTime date)
    {
        return date.ToString("MMMM");
    }
}

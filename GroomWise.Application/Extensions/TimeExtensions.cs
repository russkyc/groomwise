// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;

namespace GroomWise.Application.Extensions;

public static class TimeExtensions
{
    public static bool IsBetween(this TimeOnly target, TimeOnly start, TimeOnly end)
    {
        if (start <= end)
        {
            return start <= target && target <= end;
        }
        return start <= target || target <= end;
    }

    public static bool IsOverlapping(this Appointment appointment, TimeOnly start, TimeOnly end)
    {
        return appointment.StartTime < end && appointment.EndTime > start;
    }
}

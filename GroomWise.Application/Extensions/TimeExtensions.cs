// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

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
        if (appointment.StartTime == end)
        {
            return false;
        }

        if (appointment.EndTime == start)
        {
            return false;
        }

        return appointment.StartTime < end && appointment.EndTime > start;
    }
}

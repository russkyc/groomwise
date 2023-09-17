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
    public static bool IsBetween(this TimeOnly target, TimeOnly start, TimeOnly end) =>
        start < end ? start <= target && target <= end : start <= target || target <= end;

    public static bool IsOverlapping(this Appointment appointment, TimeOnly start, TimeOnly end) =>
        appointment.StartTime != end
        && appointment.EndTime != end
        && (appointment.StartTime < end && appointment.EndTime > start);
}

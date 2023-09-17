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

using System.Linq.Expressions;
using GroomWise.Domain.Entities;

namespace GroomWise.Domain.Filters;

public static class AppointmentFilters
{
    public static readonly Expression<Func<Appointment, bool>> IsToday = appointment =>
        appointment.Date == DateTime.Today;

    public static readonly Expression<Func<Appointment, bool>> IsThisWeek = appointment =>
        appointment.Date > DateTime.Today && appointment.Date < DateTime.Today.AddDays(7);
}

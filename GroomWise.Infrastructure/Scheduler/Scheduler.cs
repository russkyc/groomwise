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

using FluentScheduler;
using GroomWise.Infrastructure.Scheduler.Enums;
using GroomWise.Infrastructure.Scheduler.Interfaces;
using Injectio.Attributes;

namespace GroomWise.Infrastructure.Scheduler;

[RegisterSingleton<IScheduler, Scheduler>]
public class Scheduler : IScheduler
{
    public Scheduler()
    {
        JobManager.Initialize();
    }

    public void ScheduleTask(Action job, int interval, Span span)
    {
        JobManager.AddJob(
            job,
            schedule =>
            {
                if (span is Span.Seconds)
                {
                    schedule.ToRunEvery(interval).Seconds();
                    return;
                }
                if (span is Span.Minutes)
                {
                    schedule.ToRunEvery(interval).Minutes();
                    return;
                }

                if (span is Span.Hours)
                {
                    schedule.ToRunEvery(interval).Hours();
                    return;
                }

                if (span is Span.Days)
                {
                    schedule.ToRunEvery(interval).Days();
                    return;
                }

                if (span is Span.Weeks)
                {
                    schedule.ToRunEvery(interval).Weeks();
                    return;
                }

                if (span is Span.Weekdays)
                {
                    schedule.ToRunEvery(interval).Weekdays();
                    return;
                }

                schedule.ToRunEvery(interval).Months();
            }
        );
    }
}

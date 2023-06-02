// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is

namespace GroomWise.Services.App;

public class SchedulerService : ISchedulerService
{
    public void RunPeriodically(
        Func<Task> action,
        TimeSpan interval,
        CancellationToken cancellationToken = default
    )
    {
        Task.Run(
            async () =>
            {
                while (true)
                {
                    await action();
                    await Task.Delay(interval);
                }
            },
            cancellationToken
        );
    }
}

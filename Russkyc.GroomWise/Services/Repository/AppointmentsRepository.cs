// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class AppointmentsRepository : IAppointmentsRepository
{
    private readonly IDatabaseServiceAsync _databaseService;

    public AppointmentsRepository(IDatabaseServiceAsync databaseService)
    {
        _databaseService = databaseService;
    }

    public bool Add(Appointment item)
    {
        return Task.Run(
            async () => await _databaseService.Add(item)
        ).Result;
    }

    public bool AddMultiple(ICollection<Appointment> items)
    {
        return Task.Run(
            async () => await _databaseService.AddMultiple(items)
        ).Result;
    }

    public Appointment Get(Expression<Func<Appointment, bool>> filter)
    {
        return Task.Run(
            async () => await _databaseService.Get(filter)
        ).Result;
    }

    public ICollection<Appointment> GetMultiple(Expression<Func<Appointment, bool>> filter)
    {
        return Task.Run(
            async () => await _databaseService.GetMultiple(filter)
        ).Result;
    }

    public ICollection<Appointment> GetCollection()
    {
        return Task.Run(
            async () => await _databaseService.GetCollection<Appointment>()
        ).Result;
    }

    public bool Update(Expression<Func<Appointment, bool>> filter, Expression<Func<Appointment, Appointment>> action)
    {
        return Task.Run(
            async () => await _databaseService.Update(filter, action)
        ).Result;
    }

    public bool Delete(Expression<Func<Appointment, bool>> filter)
    {
        return Task.Run(
            async () => await _databaseService.Delete(filter)
        ).Result;
    }
}
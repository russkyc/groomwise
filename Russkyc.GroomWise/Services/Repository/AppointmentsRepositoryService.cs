// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class AppointmentsRepositoryService : IAppointmentsRepositoryService
{
    private readonly IDatabaseService _databaseService;

    public AppointmentsRepositoryService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public bool Add(Appointment item)
    {
        return _databaseService.Add(item);
    }

    public bool AddMultiple(ICollection<Appointment> items)
    {
        return _databaseService.AddMultiple(items);
    }

    public Appointment Get(Expression<Func<Appointment, bool>> filter)
    {
        return _databaseService.Get(filter);
    }

    public ICollection<Appointment> GetMultiple(Expression<Func<Appointment, bool>> filter)
    {
        return _databaseService.GetMultiple(filter);
    }

    public ICollection<Appointment> GetCollection()
    {
        return _databaseService.GetCollection<Appointment>();
    }

    public bool Update(Expression<Func<Appointment, bool>> filter, Expression<Func<Appointment, Appointment>> action)
    {
        return _databaseService.Update(filter, action);
    }

    public bool Delete(Expression<Func<Appointment, bool>> filter)
    {
        return _databaseService.Delete(filter);
    }
}
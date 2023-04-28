// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace Russkyc.GroomWise.Repositories;

public class AppointmentsRepository : IAppointmentsRepository
{
    private IDatabaseService _databaseService;

    public AppointmentsRepository(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    
    public bool Insert(IAppointment item)
    {
        return _databaseService.Insert(item);
    }

    public bool Insert(ICollection<IAppointment> items)
    {
        return _databaseService.Insert(items);
    }

    public IAppointment Get(Func<IAppointment, bool> filter)
    {
        return _databaseService.Get(filter);
    }

    public ICollection<IAppointment> GetAll(Func<IAppointment, bool> filter)
    {
        return _databaseService.GetAll(filter);
    }

    public ICollection<IAppointment> GetCollection()
    {
        return _databaseService.GetCollection<IAppointment>();
    }

    public bool Update(Func<IAppointment, bool> filter, Action<IAppointment> action)
    {
        return _databaseService.Update(filter, action);
    }

    public bool Delete(Func<IAppointment, bool> filter)
    {
        return _databaseService.Delete(filter);
    }
}
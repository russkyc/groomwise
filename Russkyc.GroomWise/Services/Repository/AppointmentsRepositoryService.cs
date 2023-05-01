// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class AppointmentsRepositoryService : IAppointmentsRepositoryService
{
    private IDatabaseService _databaseService;

    public AppointmentsRepositoryService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public bool Add(IAppointment item)
    {
        throw new NotImplementedException();
    }

    public bool AddMultiple(ICollection<IAppointment> item)
    {
        throw new NotImplementedException();
    }

    public IAppointment Get(Func<IAppointment, bool> filter)
    {
        throw new NotImplementedException();
    }

    public ICollection<IAppointment> GetMultiple(Func<IAppointment, bool> filter)
    {
        throw new NotImplementedException();
    }

    public ICollection<IAppointment> GetCollection()
    {
        throw new NotImplementedException();
    }

    public bool Update(Func<IAppointment, bool> filter, Action<IAppointment> action)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Func<IAppointment, bool> filter)
    {
        throw new NotImplementedException();
    }
}
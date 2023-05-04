// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
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

    public bool Add(Appointment item)
    {
        throw new NotImplementedException();
    }

    public bool AddMultiple(ICollection<Appointment> item)
    {
        throw new NotImplementedException();
    }

    public Appointment Get(Func<Appointment, bool> filter)
    {
        throw new NotImplementedException();
    }

    public ICollection<Appointment> GetMultiple(Func<Appointment, bool> filter)
    {
        throw new NotImplementedException();
    }

    public ICollection<Appointment> GetCollection()
    {
        throw new NotImplementedException();
    }

    public bool Update(Func<Appointment, bool> filter, Func<Appointment, Appointment> action)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Func<Appointment, bool> filter)
    {
        throw new NotImplementedException();
    }
}
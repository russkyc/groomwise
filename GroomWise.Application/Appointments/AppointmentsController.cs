// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;
using GroomWise.Application.Appointments.Interfaces;
using GroomWise.Domain.Entities;
using GroomWise.Infrastructure.Data.Interfaces;

namespace GroomWise.Application.Appointments;

public class AppointmentsController : IAppointmentsController
{
    private readonly IRepository<Appointment> _repository;

    public AppointmentsController(IRepository<Appointment> repository)
    {
        _repository = repository;
    }

    public void Schedule(
        DateTime date,
        Pet pet,
        Customer customer,
        GroomingService service,
        IEnumerable<Employee>? employees = null
    )
    {
        _repository.Create(
            new Appointment
            {
                Date = date,
                Pet = pet,
                Customer = customer,
                Service = service,
                Employees = employees
            }
        );
    }

    public void Update(Appointment? appointment, Action<Appointment> set)
    {
        if (appointment is null)
        {
            return;
        }

        set(appointment);
        _repository.Update(appointment);
    }

    public void Delete(Appointment? appointment)
    {
        if (appointment is null)
        {
            return;
        }

        _repository.Delete(appointment.Id);
    }

    public Appointment? Find(Expression<Func<Appointment, bool>> filter)
    {
        return _repository.Search(filter);
    }

    public IEnumerable<Appointment>? GetAll(Expression<Func<Appointment, bool>>? filter)
    {
        if (filter is null)
        {
            return _repository.GetAll();
        }
        return _repository.FindAll(filter);
    }
}

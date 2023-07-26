// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;
using GroomWise.Domain.Entities;

namespace GroomWise.Application.Appointments.Interfaces;

public interface IAppointmentsController
{
    void Schedule(
        DateTime date,
        Pet pet,
        Customer customer,
        GroomingService service,
        IEnumerable<Employee>? employees = null
    );

    void Update(Appointment? appointment, Action<Appointment> set);
    void Delete(Appointment? appointment);
    Appointment? Find(Expression<Func<Appointment, bool>> filter);
    IEnumerable<Appointment>? GetAll(Expression<Func<Appointment, bool>>? filter);
}
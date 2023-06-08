// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Data;

public class ContextManager : IContextManager
{
    private readonly PetRepository _petsRepository;
    private readonly AccountRepository _accountRepository;
    private readonly EmployeeRepository _employeesRepository;
    private readonly CustomerRepository _customersRepository;
    private readonly AppointmentRepository _appointmentRepository;
    private readonly GroomingServiceRepository _groomingServicesRepository;

    public ContextManager(
        PetRepository petsRepository,
        AccountRepository accountRepository,
        EmployeeRepository employeesRepository,
        CustomerRepository customersRepository,
        AppointmentRepository appointmentRepository,
        GroomingServiceRepository groomingServicesRepository
    )
    {
        _petsRepository = petsRepository;
        _accountRepository = accountRepository;
        _customersRepository = customersRepository;
        _employeesRepository = employeesRepository;
        _appointmentRepository = appointmentRepository;
        _groomingServicesRepository = groomingServicesRepository;
    }

    public void WriteChanges()
    {
        _petsRepository.WriteToDb();
        _accountRepository.WriteToDb();
        _customersRepository.WriteToDb();
        _employeesRepository.WriteToDb();
        _appointmentRepository.WriteToDb();
        _groomingServicesRepository.WriteToDb();
    }
}

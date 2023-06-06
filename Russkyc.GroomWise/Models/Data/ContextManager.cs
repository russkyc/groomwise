// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Data;

public class ContextManager : IContextManager
{
    private readonly PetRepository _petsRepository;
    private readonly AccountsRepository _accountsRepository;
    private readonly EmployeeRepository _employeesRepository;
    private readonly CustomerRepository _customersRepository;
    private readonly AppointmentsRepository _appointmentsRepository;
    private readonly GroomingServiceRepository _groomingServicesRepository;

    public ContextManager(
        PetRepository petsRepository,
        AccountsRepository accountsRepository,
        EmployeeRepository employeesRepository,
        CustomerRepository customersRepository,
        AppointmentsRepository appointmentsRepository,
        GroomingServiceRepository groomingServicesRepository
    )
    {
        _petsRepository = petsRepository;
        _accountsRepository = accountsRepository;
        _customersRepository = customersRepository;
        _employeesRepository = employeesRepository;
        _appointmentsRepository = appointmentsRepository;
        _groomingServicesRepository = groomingServicesRepository;
    }

    public void WriteChanges()
    {
        _petsRepository.WriteToDb();
        _accountsRepository.WriteToDb();
        _customersRepository.WriteToDb();
        _employeesRepository.WriteToDb();
        _appointmentsRepository.WriteToDb();
        _groomingServicesRepository.WriteToDb();
    }
}

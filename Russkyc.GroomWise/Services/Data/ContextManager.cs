// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data;

public class ContextManager : IContextManager
{
    private readonly Repository<Pet> _petsRepository;
    private readonly Repository<Account> _accountsRepository;
    private readonly Repository<Employee> _employeesRepository;
    private readonly Repository<Customer> _customersRepository;
    private readonly Repository<Appointment> _appointmentsRepository;
    private readonly Repository<GroomingService> _groomingServicesRepository;

    public ContextManager(
        Repository<Pet> petsRepository,
        Repository<Account> accountsRepository,
        Repository<Customer> customersRepository,
        Repository<Employee> employeesRepository,
        Repository<Appointment> appointmentsRepository,
        Repository<GroomingService> groomingServicesRepository
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

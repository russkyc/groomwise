// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data;

public class UnitOfWork
{
    public UnitOfWork(IDatabaseServiceAsync databaseService, IEncryptionService encryptionService)
    {
        _databaseService = databaseService;
        _encryptionService = encryptionService;

        _addressRepository = new AddressRepository(databaseService);
        _appointmentEmployeeRepository = new AppointmentEmployeeRepository(databaseService);
        _appointmentPetRepository = new AppointmentPetRepository(databaseService);
        _appointmentRepository = new AppointmentRepository(databaseService);
        _appointmentServiceProductRepository = new AppointmentServiceProductRepository(
            databaseService
        );
        _appointmentServiceRepository = new AppointmentServiceRepository(databaseService);
        _customerAddressRepository = new CustomerAddressRepository(databaseService);
        _customerPetRepository = new CustomerPetRepository(databaseService);
        _customerRepository = new CustomerRepository(databaseService);
        _employeeAccountRepository = new EmployeeAccountRepository(databaseService);
        _employeeAddressRepository = new EmployeeAddressRepository(databaseService);
        _employeeRepository = new EmployeeRepository(databaseService, encryptionService);
        _employeeRoleRepository = new EmployeeRoleRepository(databaseService);
        _groomingServiceRepository = new GroomingServiceRepository(databaseService);
        _petRepository = new PetRepository(databaseService);
        _productRepository = new ProductRepository(databaseService);
        _roleRepository = new RoleRepository(databaseService);
    }

    private readonly IDatabaseServiceAsync _databaseService;
    private readonly IEncryptionService _encryptionService;

    private AccountRepository? _accountsRepository;

    public AccountRepository AccountsRepository
    {
        get => _accountsRepository ??= new AccountRepository(_databaseService);
    }

    private AddressRepository? _addressRepository;

    public AddressRepository AddressRepository
    {
        get => _addressRepository ??= new AddressRepository(_databaseService);
    }

    private AppointmentEmployeeRepository? _appointmentEmployeeRepository;
    public AppointmentEmployeeRepository AppointmentEmployeeRepository
    {
        get =>
            _appointmentEmployeeRepository ??= new AppointmentEmployeeRepository(_databaseService);
    }

    private AppointmentPetRepository? _appointmentPetRepository;
    public AppointmentPetRepository AppointmentPetRepository
    {
        get => _appointmentPetRepository ??= new AppointmentPetRepository(_databaseService);
    }

    private AppointmentRepository? _appointmentRepository;
    public AppointmentRepository AppointmentRepository
    {
        get => _appointmentRepository ??= new AppointmentRepository(_databaseService);
    }

    private AppointmentServiceProductRepository? _appointmentServiceProductRepository;
    public AppointmentServiceProductRepository AppointmentServiceProductRepository
    {
        get =>
            _appointmentServiceProductRepository ??= new AppointmentServiceProductRepository(
                _databaseService
            );
    }

    private AppointmentServiceRepository? _appointmentServiceRepository;
    public AppointmentServiceRepository AppointmentServiceRepository
    {
        get => _appointmentServiceRepository ??= new AppointmentServiceRepository(_databaseService);
    }

    private CustomerAddressRepository? _customerAddressRepository;
    public CustomerAddressRepository CustomerAddressRepository
    {
        get => _customerAddressRepository ??= new CustomerAddressRepository(_databaseService);
    }

    private CustomerPetRepository? _customerPetRepository;
    public CustomerPetRepository CustomerPetRepository
    {
        get => _customerPetRepository ??= new CustomerPetRepository(_databaseService);
    }

    private CustomerRepository? _customerRepository;
    public CustomerRepository CustomerRepository
    {
        get => _customerRepository ??= new CustomerRepository(_databaseService);
    }

    private EmployeeAccountRepository? _employeeAccountRepository;
    public EmployeeAccountRepository EmployeeAccountRepository
    {
        get => _employeeAccountRepository ??= new EmployeeAccountRepository(_databaseService);
    }

    private EmployeeAddressRepository? _employeeAddressRepository;
    public EmployeeAddressRepository EmployeeAddressRepository
    {
        get => _employeeAddressRepository ??= new EmployeeAddressRepository(_databaseService);
    }

    private EmployeeRepository? _employeeRepository;
    public EmployeeRepository EmployeeRepository
    {
        get => _employeeRepository ??= new EmployeeRepository(_databaseService, _encryptionService);
    }

    private EmployeeRoleRepository? _employeeRoleRepository;
    public EmployeeRoleRepository EmployeeRoleRepository
    {
        get => _employeeRoleRepository ??= new EmployeeRoleRepository(_databaseService);
    }

    private GroomingServiceRepository? _groomingServiceRepository;
    public GroomingServiceRepository GroomingServiceRepository
    {
        get => _groomingServiceRepository ??= new GroomingServiceRepository(_databaseService);
    }

    private PetRepository? _petRepository;
    public PetRepository PetRepository
    {
        get => _petRepository ??= new PetRepository(_databaseService);
    }

    private ProductRepository? _productRepository;
    public ProductRepository ProductRepository
    {
        get => _productRepository ??= new ProductRepository(_databaseService);
    }

    private RoleRepository? _roleRepository;
    public RoleRepository RoleRepository
    {
        get => _roleRepository ??= new RoleRepository(_databaseService);
    }

    public void SaveChanges()
    {
        _addressRepository?.WriteToDb();
        _appointmentEmployeeRepository?.WriteToDb();
        _appointmentPetRepository?.WriteToDb();
        _appointmentRepository?.WriteToDb();
        _appointmentServiceProductRepository?.WriteToDb();
        _appointmentServiceRepository?.WriteToDb();
        _customerAddressRepository?.WriteToDb();
        _customerPetRepository?.WriteToDb();
        _customerRepository?.WriteToDb();
        _employeeAccountRepository?.WriteToDb();
        _employeeAddressRepository?.WriteToDb();
        _employeeRepository?.WriteToDb();
        _employeeRoleRepository?.WriteToDb();
        _groomingServiceRepository?.WriteToDb();
        _petRepository?.WriteToDb();
        _productRepository?.WriteToDb();
        _roleRepository?.WriteToDb();
    }
}

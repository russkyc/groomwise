// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data;

public class DbContext : IDbContext
{
    private readonly IDatabaseServiceAsync _databaseService;

    public DbContext(IDatabaseServiceAsync databaseService)
    {
        _databaseService = databaseService;
    }

    private AccountRepository? _accountsRepository;
    public AccountRepository AccountsRepository =>
        _accountsRepository ??= new AccountRepository(_databaseService);

    private AddressRepository? _addressRepository;
    public AddressRepository AddressRepository =>
        _addressRepository ??= new AddressRepository(_databaseService);

    private AppointmentEmployeeRepository? _appointmentEmployeeRepository;
    public AppointmentEmployeeRepository AppointmentEmployeeRepository =>
        _appointmentEmployeeRepository ??= new AppointmentEmployeeRepository(_databaseService);

    private AppointmentPetRepository? _appointmentPetRepository;
    public AppointmentPetRepository AppointmentPetRepository =>
        _appointmentPetRepository ??= new AppointmentPetRepository(_databaseService);

    private AppointmentRepository? _appointmentRepository;
    public AppointmentRepository AppointmentRepository =>
        _appointmentRepository ??= new AppointmentRepository(_databaseService);

    private AppointmentServiceProductRepository? _appointmentServiceProductRepository;
    public AppointmentServiceProductRepository AppointmentServiceProductRepository =>
        _appointmentServiceProductRepository ??= new AppointmentServiceProductRepository(
            _databaseService
        );

    private AppointmentServiceRepository? _appointmentServiceRepository;
    public AppointmentServiceRepository AppointmentServiceRepository =>
        _appointmentServiceRepository ??= new AppointmentServiceRepository(_databaseService);

    private CustomerAddressRepository? _customerAddressRepository;
    public CustomerAddressRepository CustomerAddressRepository =>
        _customerAddressRepository ??= new CustomerAddressRepository(_databaseService);

    private CustomerPetRepository? _customerPetRepository;
    public CustomerPetRepository CustomerPetRepository =>
        _customerPetRepository ??= new CustomerPetRepository(_databaseService);

    private CustomerRepository? _customerRepository;
    public CustomerRepository CustomerRepository =>
        _customerRepository ??= new CustomerRepository(_databaseService);

    private ContactInfoRepository? _contactInfoRepository;
    public ContactInfoRepository ContactInfoRepository =>
        _contactInfoRepository ??= new ContactInfoRepository(_databaseService);

    private CustomerContactInfoRepository? _customerContactInfoRepository;

    public CustomerContactInfoRepository CustomerContactInfoRepository =>
        _customerContactInfoRepository ??= new CustomerContactInfoRepository(_databaseService);

    private EmployeeAccountRepository? _employeeAccountRepository;
    public EmployeeAccountRepository EmployeeAccountRepository =>
        _employeeAccountRepository ??= new EmployeeAccountRepository(_databaseService);

    private EmployeeAddressRepository? _employeeAddressRepository;
    public EmployeeAddressRepository EmployeeAddressRepository =>
        _employeeAddressRepository ??= new EmployeeAddressRepository(_databaseService);

    private EmployeeRepository? _employeeRepository;
    public EmployeeRepository EmployeeRepository =>
        _employeeRepository ??= new EmployeeRepository(_databaseService);

    private EmployeeRoleRepository? _employeeRoleRepository;
    public EmployeeRoleRepository EmployeeRoleRepository =>
        _employeeRoleRepository ??= new EmployeeRoleRepository(_databaseService);

    private GroomingServiceRepository? _groomingServiceRepository;
    public GroomingServiceRepository GroomingServiceRepository =>
        _groomingServiceRepository ??= new GroomingServiceRepository(_databaseService);

    private PetRepository? _petRepository;
    public PetRepository PetRepository => _petRepository ??= new PetRepository(_databaseService);

    private ProductRepository? _productRepository;
    public ProductRepository ProductRepository =>
        _productRepository ??= new ProductRepository(_databaseService);

    private RoleRepository? _roleRepository;
    public RoleRepository RoleRepository =>
        _roleRepository ??= new RoleRepository(_databaseService);

    public void SaveChanges()
    {
        _addressRepository?.WriteToDb();
        _appointmentEmployeeRepository?.WriteToDb();
        _appointmentPetRepository?.WriteToDb();
        _appointmentRepository?.WriteToDb();
        _appointmentServiceProductRepository?.WriteToDb();
        _appointmentServiceRepository?.WriteToDb();
        _contactInfoRepository?.WriteToDb();
        _customerContactInfoRepository?.WriteToDb();
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

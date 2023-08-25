// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.Service;

public interface IDbContext
{
    AccountRepository AccountsRepository { get; }
    AddressRepository AddressRepository { get; }
    AppointmentEmployeeRepository AppointmentEmployeeRepository { get; }
    AppointmentPetRepository AppointmentPetRepository { get; }
    AppointmentRepository AppointmentRepository { get; }
    AppointmentServiceProductRepository AppointmentServiceProductRepository { get; }
    AppointmentServiceRepository AppointmentServiceRepository { get; }
    CustomerAddressRepository CustomerAddressRepository { get; }
    CustomerPetRepository CustomerPetRepository { get; }
    CustomerRepository CustomerRepository { get; }
    ContactInfoRepository ContactInfoRepository { get; }
    CustomerContactInfoRepository CustomerContactInfoRepository { get; }
    EmployeeAccountRepository EmployeeAccountRepository { get; }
    EmployeeAddressRepository EmployeeAddressRepository { get; }
    EmployeeRepository EmployeeRepository { get; }
    EmployeeRoleRepository EmployeeRoleRepository { get; }
    GroomingServiceRepository GroomingServiceRepository { get; }
    PetRepository PetRepository { get; }
    ProductRepository ProductRepository { get; }
    RoleRepository RoleRepository { get; }
    void SaveChanges();
}

// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels.Customers;

public partial class AddCustomersViewModel : ViewModelBase, IAddCustomersViewModel
{
    private readonly ILogger _logger;
    private readonly IDbContext _dbContext;
    private readonly IFactory<Customer> _customerFactory;
    private readonly IFactory<DialogView> _dialogFactory;

    [ObservableProperty]
    private string _firstName;

    [ObservableProperty]
    private string _middleName;

    [ObservableProperty]
    private string _lastName;

    [ObservableProperty]
    private string _primaryAddress;

    [ObservableProperty]
    private string _secondaryAddress;

    [ObservableProperty]
    private string _contactNumber;

    [ObservableProperty]
    private string _email;

    public AddCustomersViewModel(
        ILogger logger,
        IDbContext dbContext,
        IFactory<DialogView> dialogFactory,
        IFactory<Customer> customerFactory
    )
    {
        _logger = logger;
        _dbContext = dbContext;
        _dialogFactory = dialogFactory;
        _customerFactory = customerFactory;
    }

    [RelayCommand]
    void AddCustomer()
    {
        var customerId = _dbContext.CustomerRepository.GetLastId().Increment();
        var customer = _customerFactory.Create(customer =>
        {
            customer.Id = customerId;
            customer.FirstName = FirstName;
            customer.MiddleName = MiddleName;
            customer.LastName = LastName;
        });
        var addressId = _dbContext.AddressRepository.GetLastId().Increment();
        var address = new Address
        {
            Id = addressId,
            PrimaryAddress = PrimaryAddress,
            SecondaryAddress = SecondaryAddress
        };
        var customerAddress = new CustomerAddress
        {
            CustomerId = customerId,
            AddressId = addressId
        };
        var contactInfoId = _dbContext.ContactInfoRepository.GetLastId().Increment();
        var contactInfo = new ContactInfo
        {
            Id = contactInfoId,
            ContactNumber = ContactNumber,
            Email = Email
        };

        var customerContactInfo = new CustomerContactInfo
        {
            CustomerId = customerId,
            ContactInfoId = contactInfoId
        };

        _dbContext.CustomerRepository.Add(customer);
        _dbContext.AddressRepository.Add(address);
        _dbContext.CustomerAddressRepository.Add(customerAddress);
        _dbContext.ContactInfoRepository.Add(contactInfo);
        _dbContext.CustomerContactInfoRepository.Add(customerContactInfo);

        ClearFields();
    }

    void ClearFields()
    {
        FirstName = string.Empty;
        MiddleName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        ContactNumber = string.Empty;
        PrimaryAddress = string.Empty;
        SecondaryAddress = string.Empty;
    }
}

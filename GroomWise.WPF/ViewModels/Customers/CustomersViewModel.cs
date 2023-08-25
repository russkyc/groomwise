// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels.Customers;

public partial class CustomersViewModel : ViewModelBase, ICustomersViewModel
{
    private readonly ILogger _logger;
    private readonly IDbContext _dbContext;
    private readonly IFactory<AddCustomersView> _addCustomersViewFactory;
    private readonly IFactory<CustomerCardViewModel> _customerCardViewModelFactory;

    [ObservableProperty]
    private CustomerCardViewModel? _customerInfo;

    [ObservableProperty]
    private SynchronizedObservableCollection<CustomerCardViewModel> _customers;

    public CustomersViewModel(
        ILogger logger,
        IDbContext dbContext,
        IFactory<CustomerCardViewModel> customerCardViewModelFactory,
        IFactory<AddCustomersView> addCustomersViewFactory
    )
    {
        _logger = logger;
        _dbContext = dbContext;
        _customerCardViewModelFactory = customerCardViewModelFactory;
        _addCustomersViewFactory = addCustomersViewFactory;

        Customers = new SynchronizedObservableCollection<CustomerCardViewModel>();
        ReloadCustomers();
    }

    [RelayCommand]
    void GetCustomerInfo(CustomerCardViewModel customer)
    {
        CustomerInfo = customer;
    }

    public void ReloadCustomers()
    {
        Task.Run(() =>
        {
            var customers = new List<CustomerCardViewModel>();
            _dbContext.CustomerRepository
                .GetAll()
                .ToList()
                .ForEach(customer =>
                {
                    customers.Add(
                        _customerCardViewModelFactory.Create(customervm =>
                        {
                            customervm.Id = customer.Id;

                            customervm.Name = $"{customer.FirstName} {customer.LastName}";

                            var customerAddress = _dbContext.CustomerAddressRepository.Find(
                                address => address.CustomerId == customer.Id
                            );

                            if (customerAddress == null)
                                return;

                            var address = _dbContext.AddressRepository.Find(
                                address => address.Id == customerAddress.AddressId
                            );

                            if (address == null)
                                return;

                            customervm.Address = address.PrimaryAddress;

                            var customerContactInfo = _dbContext.CustomerContactInfoRepository.Find(
                                address => address.CustomerId == customer.Id
                            );

                            if (customerContactInfo == null)
                                return;

                            var contactInfo = _dbContext.ContactInfoRepository.Find(
                                contactInfo => contactInfo.Id == customerContactInfo.ContactInfoId
                            );

                            if (contactInfo == null)
                                return;

                            customervm.ContactNumber = contactInfo.ContactNumber;
                            customervm.Email = contactInfo.Email;

                            var customerPets = _dbContext.CustomerPetRepository
                                .FindAll(pet => pet.OwnerId == customer.Id)
                                .ToList();

                            if (customerPets == null)
                                return;

                            customerPets.ForEach(customerPet =>
                            {
                                var pet = _dbContext.PetRepository.Find(
                                    pet => pet.Id == customerPet.PetId
                                );

                                if (pet == null)
                                    return;

                                customervm.Pets.Add(pet);
                            });
                        })
                    );
                });
            var command = new SynchronizeCollectionCommand<
                CustomerCardViewModel,
                SynchronizedObservableCollection<CustomerCardViewModel>
            >(ref _customers, customers);
            command.Execute();
        });
    }

    [RelayCommand]
    void AddCustomer()
    {
        _addCustomersViewFactory.Create().Show();
    }
}

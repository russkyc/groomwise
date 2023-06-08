// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class CustomersViewModel : ViewModelBase, ICustomersViewModel
{
    private readonly ILogger _logger;
    private readonly CustomerRepository _customerRepository;
    private readonly CustomerPetRepository _customerPetRepository;
    private readonly CustomerAddressRepository _customerAddressRepository;
    private readonly AddressRepository _addressRepository;
    private readonly PetRepository _petRepository;
    private readonly CustomerCardViewModelFactory _customerCardViewModelFactory;

    [ObservableProperty]
    private SynchronizedObservableCollection<CustomerCardViewModel> _customers;

    public CustomersViewModel(
        ILogger logger,
        CustomerRepository customerRepository,
        PetRepository petRepository,
        CustomerPetRepository customerPetRepository,
        CustomerAddressRepository customerAddressRepository,
        AddressRepository addressRepository,
        CustomerCardViewModelFactory customerCardViewModelFactory
    )
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _petRepository = petRepository;
        _customerPetRepository = customerPetRepository;
        _customerAddressRepository = customerAddressRepository;
        _addressRepository = addressRepository;
        _customerCardViewModelFactory = customerCardViewModelFactory;

        Customers = new SynchronizedObservableCollection<CustomerCardViewModel>();
        GetCustomers();
    }

    void GetCustomers()
    {
        Task.Run(() =>
        {
            var customers = new List<CustomerCardViewModel>();
            _customerRepository
                .GetAll()
                .ToList()
                .ForEach(customer =>
                {
                    customers.Add(
                        _customerCardViewModelFactory.Create(customervm =>
                        {
                            customervm.Id = customer.Id;

                            customervm.Name = $"{customer.FirstName} {customer.LastName}";

                            var customerAddress = _customerAddressRepository.Find(
                                address => address.CustomerId == customer.Id
                            );

                            if (customerAddress == null)
                                return;

                            var address = _addressRepository.Find(
                                address => address.Id == customerAddress.AddressId
                            );

                            if (address == null)
                                return;

                            customervm.Address =
                                $"{address.Barangay}, {address.City}, {address.Province}";

                            var customerPets = _customerPetRepository
                                .FindAll(pet => pet.OwnerId == customer.Id)
                                .ToList();

                            if (customerPets == null)
                                return;

                            customerPets.ForEach(customerPet =>
                            {
                                var pet = _petRepository.Find(pet => pet.Id == customerPet.PetId);

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
}

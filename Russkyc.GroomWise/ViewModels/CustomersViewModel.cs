// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class CustomersViewModel : ViewModelBase, ICustomersViewModel
{
    private readonly ILogger _logger;
    private readonly CustomerRepository _customerRepository;

    [ObservableProperty]
    private CustomersCollection _customers;

    public CustomersViewModel(ILogger logger, CustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;

        Customers = new CustomersCollection();
        GetCustomers();
    }

    void GetCustomers()
    {
        Task.Run(() =>
        {
            var command = new SynchronizeCollectionCommand<Customer, CustomersCollection>(
                ref _customers,
                _customerRepository.GetAll().ToList()
            );
            command.Execute();
        });
    }
}

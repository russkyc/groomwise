// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels.Customers;

public partial class AddCustomersViewModel : ViewModelBase, IAddCustomersViewModel
{
    private readonly ILogger _logger;
    private readonly IDbContext _dbContext;

    [ObservableProperty]
    private string _firstName;

    [ObservableProperty]
    private string _middleName;

    [ObservableProperty]
    private string _lastName;

    [ObservableProperty]
    private string _province;

    [ObservableProperty]
    private string _city;

    [ObservableProperty]
    private string _barangay;

    [ObservableProperty]
    private string _houseNumber;

    [ObservableProperty]
    private string _zipCode;

    public AddCustomersViewModel(ILogger logger, IDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
}

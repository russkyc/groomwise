// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public class AddCustomersViewModel : ViewModelBase, IAddCustomersViewModel
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _dbContext;

    public AddCustomersViewModel(ILogger logger, IUnitOfWork dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
}

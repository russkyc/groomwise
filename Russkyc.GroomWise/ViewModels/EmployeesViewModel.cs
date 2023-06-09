// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class EmployeesViewModel : ViewModelBase, IEmployeesViewModel
{
    private readonly IEncryptionService _encryptionService;
    private readonly ISchedulerService _schedulerService;
    private readonly ILogger _logger;

    private readonly IUnitOfWork _dbContext;

    [ObservableProperty]
    private string? _filter;

    [ObservableProperty]
    private SynchronizedObservableCollection<Employee> _employees;

    public EmployeesViewModel(
        IEncryptionService encryptionService,
        ISchedulerService schedulerService,
        IUnitOfWork dbContext,
        ILogger logger
    )
    {
        _encryptionService = encryptionService;
        _schedulerService = schedulerService;
        _dbContext = dbContext;
        _logger = logger;
        
        Employees = new SynchronizedObservableCollection<Employee>();

        GetEmployees();
    }

    void GetEmployees()
    {
        Task.Run(() =>
        {
            var command = new SynchronizeCollectionCommand<
                Employee,
                SynchronizedObservableCollection<Employee>
            >(ref _employees, _dbContext.EmployeeRepository.GetAll().ToList());
            command.Execute();
            _logger.Log(this, "Synchronized employees collection");
        });
    }
}

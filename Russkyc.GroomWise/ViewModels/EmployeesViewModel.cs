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

    private readonly EmployeeRepository _employeeRepository;

    [ObservableProperty]
    private string? _filter;

    [ObservableProperty]
    private EmployeesCollection _employees;

    public EmployeesViewModel(
        IEncryptionService encryptionService,
        EmployeeRepository employeeRepository,
        ISchedulerService schedulerService,
        ILogger logger
    )
    {
        _encryptionService = encryptionService;
        _employeeRepository = employeeRepository;
        _schedulerService = schedulerService;
        _logger = logger;
        Employees = new EmployeesCollection();

        GetEmployees();
    }

    void GetEmployees()
    {
        _schedulerService.RunPeriodically(
            () =>
            {
                var command = new SynchronizeCollectionCommand<Employee, EmployeesCollection>(
                    ref _employees,
                    _employeeRepository.GetAll().ToList()
                );
                command.Execute();
                _logger.Log(this, "Synchronized employees collection");
            },
            TimeSpan.FromSeconds(2)
        );
    }
}

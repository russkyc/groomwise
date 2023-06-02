// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class EmployeesViewModel : ViewModelBase, IEmployeesViewModel
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly ISchedulerService _schedulerService;

    [ObservableProperty]
    private string _filter;

    [ObservableProperty]
    private EmployeesCollection _employees;

    public EmployeesViewModel(
        IEncryptionService encryptionService,
        IEmployeeRepository employeeRepository,
        ISchedulerService schedulerService
    )
    {
        _encryptionService = encryptionService;
        _employeeRepository = employeeRepository;
        _schedulerService = schedulerService;
        Employees = new EmployeesCollection();

        GetEmployees();
    }

    void GetEmployees()
    {
        Task.Run(async () =>
        {
            await DispatchHelper.UiInvokeAsync(
                () =>
                    Employees.AddRange(
                        _employeeRepository
                            .GetCollection()
                            .Select(employee => _encryptionService.Decrypt(employee))
                            .ToList()
                    )
            );
        });
    }
}

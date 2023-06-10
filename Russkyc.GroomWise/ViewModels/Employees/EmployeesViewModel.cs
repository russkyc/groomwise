// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels.Employees;

public partial class EmployeesViewModel : ViewModelBase, IEmployeesViewModel
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _dbContext;
    private readonly IFactory<EmployeeCardViewModel> _employeeCardViewModelFactory;

    [ObservableProperty]
    private string? _filter;

    [ObservableProperty]
    private SynchronizedObservableCollection<EmployeeCardViewModel> _employees;

    public EmployeesViewModel(
        IUnitOfWork dbContext,
        ILogger logger,
        IFactory<EmployeeCardViewModel> employeeCardViewModelFactory
    )
    {
        _logger = logger;
        _dbContext = dbContext;
        _employeeCardViewModelFactory = employeeCardViewModelFactory;

        Employees = new SynchronizedObservableCollection<EmployeeCardViewModel>();

        GetEmployees();
    }

    void GetEmployees()
    {
        Task.Run(() =>
        {
            var employees = new List<EmployeeCardViewModel>();
            _dbContext.EmployeeRepository
                .GetAll()
                .ToList()
                .ForEach(employee =>
                {
                    employees.Add(
                        _employeeCardViewModelFactory.Create(employeevm =>
                        {
                            employeevm.Id = employee.Id;

                            employeevm.Name = $"{employee.FirstName} {employee.LastName}";

                            var employeeAddress = _dbContext.EmployeeAddressRepository.Find(
                                address => address.EmployeeId == employee.Id
                            );

                            if (employeeAddress == null)
                                return;

                            var address = _dbContext.AddressRepository.Find(
                                address => address.Id == employeeAddress.AddressId
                            );

                            if (address == null)
                                return;

                            employeevm.Address =
                                $"{address.Barangay}, {address.City}, {address.Province}";

                            var employeeRoles = _dbContext.EmployeeRoleRepository.FindAll(
                                role => role.EmployeeId == employee.Id
                            );

                            if (employeeRoles == null)
                                return;

                            var roles = employeeRoles.Select(
                                employeeRole =>
                                    _dbContext.RoleRepository.Find(
                                        role => role.Id == employeeRole.RoleId
                                    )
                            );

                            if (roles == null)
                                return;

                            employeevm.Roles.AddRange(roles);
                        })
                    );
                });
            var command = new SynchronizeCollectionCommand<
                EmployeeCardViewModel,
                SynchronizedObservableCollection<EmployeeCardViewModel>
            >(ref _employees, employees);
            command.Execute();
            _logger.Log(this, "Synchronized employees collection");
        });
    }
}

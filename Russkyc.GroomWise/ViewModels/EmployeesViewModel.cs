// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class EmployeesViewModel : ViewModelBase, IEmployeesViewModel
{
    [ObservableProperty]
    private EmployeesCollection _employees;

    public EmployeesViewModel()
    {
        Employees = new EmployeesCollection();
        GetEmployees();
    }

    void GetEmployees()
    {
        Task.Run(async () =>
            {
                for (int i = 0; i < 50; i++)
                {
                    _employees.Add(
                        new Employee
                        {
                            FirstName = $"Test{i}",
                            LastName = $"Test Employee Description {i}"
                        }
                    );
                }
            })
            .ContinueWith(task => OnPropertyChanged(nameof(Employees)));
    }
}

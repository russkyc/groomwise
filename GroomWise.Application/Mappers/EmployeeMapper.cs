// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Observables;
using GroomWise.Domain.Entities;
using Mapster;

namespace GroomWise.Application.Mappers;

public static class EmployeeMapper
{
    public static Employee ToEntity(this ObservableEmployee observablePet)
    {
        return observablePet.Adapt<Employee>();
    }

    public static ObservableEmployee ToObservable(this Employee employee)
    {
        return employee.Adapt<ObservableEmployee>();
    }
}

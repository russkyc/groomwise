// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Observables;
using GroomWise.Domain.Entities;
using Lombok.NET;
using Riok.Mapperly.Abstractions;

namespace GroomWise.Application.Mappers;

[Mapper]
[Singleton]
public partial class CustomerMapper
{
    public partial ObservableCustomer CustomerToObservable(Customer customer);
}

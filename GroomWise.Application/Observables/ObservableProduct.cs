// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using Lombok.NET;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservableProduct
{
    [Property]
    private Guid _id;

    [Property]
    private string? _productName;

    [Property]
    private string? _productDescription;

    [Property]
    private int _quantity;
}

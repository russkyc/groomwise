// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class CustomerCardViewModel : ObservableObject, IEntity
{
    public int Id { get; set; }

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private string? _address;

    [ObservableProperty]
    private ObservableCollection<Pet> _pets;

    public CustomerCardViewModel()
    {
        Pets = new ObservableCollection<Pet>();
    }
}

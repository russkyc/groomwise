// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.ViewModel;

public interface ILoginViewModel
{
    /// <inheritdoc cref="LoginViewModel._applicationService"/>
    IApplicationService ApplicationService { get; set; }

    /// <inheritdoc cref="LoginViewModel._notifications"/>
    SynchronizedObservableCollection<Notification> Notifications { get; set; }

    /// <inheritdoc cref="LoginViewModel._password"/>
    string? Password { get; set; }

    /// <inheritdoc cref="LoginViewModel._username"/>
    string? Username { get; set; }
}

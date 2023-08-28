// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.ViewModels;
using GroomWise.Domain.Interfaces;
using GroomWise.Factories;
using GroomWise.Infrastructure.Authentication;
using GroomWise.Infrastructure.Authentication.Interfaces;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.Encryption;
using GroomWise.Infrastructure.Encryption.Interfaces;
using GroomWise.Views;
using GroomWise.Views.Dialogs;
using Jab;
using MvvmGen.Events;

namespace GroomWise.Containers;

[ServiceProvider]
// Infrastructure
[Singleton<GroomWiseDbContext>]
[Singleton<IEventAggregator, EventAggregator>]
[Singleton<IEncryptionService, EncryptionService>]
[Singleton<IAuthenticationService, AuthenticationService>]
// Views
[Singleton<LoginView>]
[Singleton<MainView>]
// Application
[Singleton<ILoginViewModel, LoginViewModel>]
[Singleton<IAppViewModel, AppViewModel>]
// Factory
[Singleton<IDialogFactory, DialogFactory>]
public partial class ServiceProvider { }

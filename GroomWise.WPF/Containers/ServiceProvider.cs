// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.ViewModels;
using GroomWise.Infrastructure.Authentication;
using GroomWise.Infrastructure.Authentication.Interfaces;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.Encryption;
using GroomWise.Infrastructure.Encryption.Interfaces;
using GroomWise.Views.Dialogs;
using Jab;

namespace GroomWise.Containers;

[ServiceProvider]
[Singleton<GroomWiseDbContext>()]
[Singleton<LoginView>]
[Singleton<ILoginViewModel,LoginViewModel>]
[Singleton<IEncryptionService,EncryptionService>]
[Singleton<IAuthenticationService, AuthenticationService>]
public partial class ServiceProvider { }

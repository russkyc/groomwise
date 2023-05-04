﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services;

public static class ServiceContainer
{
    public static IServicesContainer ConfigureServices()
    {
        return new ServicesCollection()

            // Add Configs
            .AddSingleton<IConfigurationService, ConfigurationService>()

            // Add Application Services
            .AddSingleton<ISessionService, SessionService>()
            .AddSingleton<IEncryptionService, EncryptionService>()
            .AddSingleton<IApplicationService, ApplicationService>()
            .AddSingleton<IThemeManagerService, ThemeManagerService>()

            // Add Factory Services
            .AddSingleton<IPetFactoryService, PetFactoryService>()
            .AddSingleton<IAccountFactoryService, AccountFactoryService>()
            .AddSingleton<IGroomerFactoryService, GroomerFactoryService>()
            .AddSingleton<ICustomerFactoryService, CustomerFactoryService>()
            .AddSingleton<IAppointmentFactoryService, AppointmentFactoryService>()
            .AddSingleton<INotificationFactoryService, NotificationFactoryService>()

            // Add Migrations
            .AddSingleton<IMigrationService, MigrationService>()

            // Add Data Services
            .AddSingleton<IDatabaseService, LiteDbDataService>()
            .AddSingleton<IAccountsRepositoryService, AccountsRepositoryService>()
            .AddSingleton<IAppointmentsRepositoryService, AppointmentsRepositoryService>()

            // Add ViewModels
            .AddSingleton<IAppointmentsViewModel, AppointmentsViewModel>()
            .AddSingleton<ICustomersViewModel, CustomersViewModel>()
            .AddSingleton<IDashboardViewModel, DashboardViewModel>()
            .AddSingleton<IPetsViewModel, PetsViewModel>()
            .AddSingleton<IReportsViewModel, ReportsViewModel>()
            .AddSingleton<IServicesViewModel, ServicesViewModel>()
            .AddSingleton<IMainViewModel, MainViewModel>()
            .AddSingleton<ILoginViewModel, LoginViewModel>()

            // Add Views
            .AddSingleton<AppointmentsView>(nameof(AppointmentsView))
            .AddSingleton<CustomersView>(nameof(CustomersView))
            .AddSingleton<DashboardView>(nameof(DashboardView))
            .AddSingleton<PetsView>(nameof(PetsView))
            .AddSingleton<ReportsView>(nameof(ReportsView))
            .AddSingleton<ServicesView>(nameof(ServicesView))
            .AddSingleton<MainView>()
            .AddSingleton<LoginView>()

            // Build Container
            .Build();

    }
}
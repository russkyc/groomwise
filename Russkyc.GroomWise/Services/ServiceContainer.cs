﻿// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services;

public static class ServiceContainer
{
    public static IServicesContainer ConfigureServices()
    {
        return new ServicesCollection()
            
            // Add Application Services
            .AddSingleton<IAppService, AppService>()
            .AddSingleton<IThemeManagerService, ThemeManagerService>()
            
            // Add Factory Services
            .AddSingleton<IPetFactoryService, PetFactoryService>()
            .AddSingleton<IAccountFactoryService, AccountFactoryService>()
            .AddSingleton<IGroomerFactoryService, GroomerFactoryService>()
            .AddSingleton<ICustomerFactoryService, CustomerFactoryService>()
            .AddSingleton<IAppointmentFactoryService, AppointmentFactoryService>()
            .AddSingleton<INotificationFactoryService,NotificationFactoryService>()
            
            // Add Data Services
            .AddSingleton<IDatabaseService, LiteDbDataService>()
            .AddSingleton<IAccountsRepositoryService, AccountsRepositoryService>()
            .AddSingleton<IAppointmentsRepositoryService, AppointmentsRepositoryService>()
            
            // Add ViewModels
            .AddTransient<IAppointmentsViewModel, AppointmentsViewModel>()
            .AddTransient<ICustomersViewModel, CustomersViewModel>()
            .AddTransient<IDashboardViewModel, DashboardViewModel>()
            .AddTransient<IPetsViewModel, PetsViewModel>()
            .AddTransient<IReportsViewModel, ReportsViewModel>()
            .AddTransient<IServicesViewModel, ServicesViewModel>()
            .AddTransient<IMainViewModel, MainViewModel>()
            .AddTransient<ILoginViewModel, LoginViewModel>()
            
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
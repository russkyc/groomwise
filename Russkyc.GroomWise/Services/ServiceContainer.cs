// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
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
            
            // Add Logger
            #if(DEBUG)
            .AddSingleton<ILogger, ConsoleAndFileLogger>()
            #else
            .AddSingleton<ILogger, DebugLogger>()
            #endif

            // Add Factory Services
            .AddSingleton<IAppointmentScheduleFactory, AppointmentScheduleFactory>()
            .AddSingleton<ILiteDatabaseFactory, LiteDatabaseFactory>()
            .AddSingleton<IMaterialIconFactory, MaterialIconFactory>()
            .AddSingleton<INavItemFactoryService, NavItemFactoryService>()
            .AddSingleton<IPetFactoryService, PetFactoryService>()
            .AddSingleton<IAccountFactoryService, AccountFactoryService>()
            .AddSingleton<IGroomerFactoryService, GroomerFactoryService>()
            .AddSingleton<ICustomerFactoryService, CustomerFactoryService>()
            .AddSingleton<IAppointmentFactoryService, AppointmentFactoryService>()
            .AddSingleton<INotificationFactoryService, NotificationFactoryService>()
            
            // Add Application Services
            .AddSingleton<ISessionManagerService, SessionManagerService>()
            .AddSingleton<IEncryptionService, EncryptionService>()
            .AddSingleton<IApplicationService, ApplicationService>()
            .AddSingleton<IThemeManagerService, ThemeManagerService>()
            
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
            .AddSingleton<IAppointmentsView, AppointmentsView>()
            .AddSingleton<ICustomersView, CustomersView>()
            .AddSingleton<IDashboardView, DashboardView>()
            .AddSingleton<IPetsView, PetsView>()
            .AddSingleton<IReportsView, ReportsView>()
            .AddSingleton<IServicesView, ServicesView>()
            .AddSingleton<IMainView, MainView>()
            .AddSingleton<ILoginView, LoginView>()

            // Build Container
            .Build();

    }
}
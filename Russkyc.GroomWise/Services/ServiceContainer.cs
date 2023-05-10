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
            // Security
            .AddSingleton<ICredentialStore, CredentialStore>()
            .AddSingleton<IEncryptionService, EncryptionService>()
            // Add Configs
            .AddSingleton<IConfigurationService, ConfigurationService>()
            // Add Logger
            .AddSingleton<ILogger, DebugAndFileLogger>()
            // Add Global Hotkey Listener
            .AddSingleton<IHotkeyListenerService, HotKeyListenerService>()
            // Add Factory Services
            .AddSingleton<IMaterialIconFactory, MaterialIconFactory>()
            .AddSingleton<INavItemFactory, NavItemFactory>()
            .AddSingleton<IPetFactory, PetFactory>()
            .AddSingleton<IEmployeeFactory, EmployeeFactory>()
            .AddSingleton<IAccountFactory, AccountFactory>()
            .AddSingleton<ICustomerFactory, CustomerFactory>()
            .AddSingleton<IAppointmentFactory, AppointmentFactory>()
            .AddSingleton<INotificationFactory, NotificationFactory>()
            .AddSingleton<IAddAppointmentsViewFactory, AddAppointmentsViewFactory>()
            .AddSingleton<IDialogFactory, DialogFactory>()
            // Add Data Services
            .AddSingleton<IConnectionSourceProvider, ConnectionSourceProvider>()
            .AddSingleton<IDatabaseServiceAsync, MySqlDataServiceAsync>()
            // Add Migrations
            .AddSingleton<IMigrationService, MigrationService>()
            // Add Application Services
            .AddSingleton<ISessionManagerService, SessionManagerService>()
            .AddSingleton<IApplicationService, ApplicationService>()
            .AddSingleton<IThemeManagerService, ThemeManagerService>()
            // Add Repository Services
            .AddSingleton<IAccountsRepository, AccountsRepository>()
            .AddSingleton<IEmployeeRepository, EmployeeRepository>()
            .AddSingleton<IAppointmentsRepository, AppointmentsRepository>()
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
            .AddSingleton<IAddAppointmentsView, AddAppointmentsView>()
            // Build Container
            .Build();
    }
}

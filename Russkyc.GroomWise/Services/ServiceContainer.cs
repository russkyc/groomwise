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
            .AddSingleton<IConfigProvider, ConfigProvider>()
            // Add Scheduler
            .AddSingleton<ISchedulerService, SchedulerService>()
            // Add Logger
            .AddSingleton<ILogger, DebugAndFileLogger>()
            // Add Global Hotkey Listener
            .AddSingleton<IHotkeyListenerService, HotKeyListenerService>()
            // Add Factory Services
            .AddSingleton<IAddAppointmentsViewFactory, AddAppointmentsViewFactory>()
            .AddSingleton<IMaterialIconFactory, MaterialIconFactory>()
            .AddSingleton<INotificationFactory, NotificationFactory>()
            .AddSingleton<IAppointmentFactory, AppointmentFactory>()
            .AddSingleton<ICustomerFactory, CustomerFactory>()
            .AddSingleton<IEmployeeFactory, EmployeeFactory>()
            .AddSingleton<INavItemFactory, NavItemFactory>()
            .AddSingleton<IAccountFactory, AccountFactory>()
            .AddSingleton<IDialogFactory, DialogFactory>()
            .AddSingleton<IPetFactory, PetFactory>()
            // Add Data Services
            .AddSingleton<IConnectionSourceProvider, ConnectionSourceProvider>()
            .AddSingleton<IDatabaseServiceAsync, DataServiceProviderAsync>()
            // Add Application Services
            .AddSingleton<ISessionManagerService, SessionManagerService>()
            .AddSingleton<IThemeManagerService, ThemeManagerService>()
            .AddSingleton<IApplicationService, ApplicationService>()
            // Add Repository Services
            .AddSingleton<IAppointmentsRepository, AppointmentsRepository>()
            .AddSingleton<IAccountsRepository, AccountsRepository>()
            .AddSingleton<IEmployeeRepository, EmployeeRepository>()
            // Add ViewModels
            .AddSingleton<IAppointmentsViewModel, AppointmentsViewModel>()
            .AddSingleton<IEmployeesViewModel, EmployeesViewModel>()
            .AddSingleton<ICustomersViewModel, CustomersViewModel>()
            .AddSingleton<IDashboardViewModel, DashboardViewModel>()
            .AddSingleton<IServicesViewModel, ServicesViewModel>()
            .AddSingleton<ISettingsViewModel, SettingsViewModel>()
            .AddSingleton<IReportsViewModel, ReportsViewModel>()
            .AddSingleton<ILoginViewModel, LoginViewModel>()
            .AddSingleton<IPetsViewModel, PetsViewModel>()
            .AddSingleton<IMainViewModel, MainViewModel>()
            // Add Views
            .AddTransient<IAddAppointmentsView, AddAppointmentsView>()
            .AddSingleton<IAppointmentsView, AppointmentsView>()
            .AddSingleton<ICustomersView, CustomersView>()
            .AddSingleton<IDashboardView, DashboardView>()
            .AddSingleton<IEmployeesView, EmployeesView>()
            .AddSingleton<ISettingsView, SettingsView>()
            .AddSingleton<IServicesView, ServicesView>()
            .AddSingleton<IReportsView, ReportsView>()
            .AddSingleton<ILoginView, LoginView>()
            .AddSingleton<IPetsView, PetsView>()
            .AddSingleton<IMainView, MainView>()
            // Build Container
            .Build();
    }
}

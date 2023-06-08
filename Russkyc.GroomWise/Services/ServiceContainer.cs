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
            // Add Factory Services
            .AddSingleton<AddAppointmentsViewFactory>()
            .AddSingleton<MaterialIconFactory>()
            .AddSingleton<NotificationFactory>()
            .AddSingleton<AppointmentFactory>()
            .AddSingleton<CustomerFactory>()
            .AddSingleton<EmployeeFactory>()
            .AddSingleton<NavItemFactory>()
            .AddSingleton<AccountFactory>()
            .AddSingleton<DialogFactory>()
            .AddSingleton<PetFactory>()
            .AddSingleton<SessionFactory>()
            // Add Data Services
            .AddSingleton<IConnectionSourceProvider, ConnectionSourceProvider>()
            .AddSingleton<IDatabaseServiceAsync, DataServiceProviderAsync>()
            // Add Repository Services
            .AddSingleton<AccountRepository>()
            .AddSingleton<AddressRepository>()
            .AddSingleton<AppointmentEmployeeRepository>()
            .AddSingleton<AppointmentPetRepository>()
            .AddSingleton<AppointmentRepository>()
            .AddSingleton<AppointmentServiceProductRepository>()
            .AddSingleton<AppointmentServiceRepository>()
            .AddSingleton<CustomerAddressRepository>()
            .AddSingleton<CustomerPetRepository>()
            .AddSingleton<CustomerRepository>()
            .AddSingleton<EmployeeAccountRepository>()
            .AddSingleton<EmployeeAddressRepository>()
            .AddSingleton<EmployeeRepository>()
            .AddSingleton<EmployeeRoleRepository>()
            .AddSingleton<GroomingServiceRepository>()
            .AddSingleton<PetRepository>()
            .AddSingleton<ProductRepository>()
            .AddSingleton<RoleRepository>()
            // Add Application Services
            .AddSingleton<ISessionManagerService, SessionManagerService>()
            .AddSingleton<IThemeManagerService, ThemeManagerService>()
            .AddSingleton<IApplicationService, ApplicationService>()
            // Context Manager
            .AddSingleton<IContextManager, ContextManager>()
            // Add ViewModels
            .AddSingleton<IAppointmentsViewModel, AppointmentsViewModel>()
            .AddSingleton<IAddAppointmentsViewModel, AddAppointmentsViewModel>()
            .AddSingleton<IInventoryViewModel, InventoryViewModel>()
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
            .AddSingleton<IInventoryView, InventoryView>()
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

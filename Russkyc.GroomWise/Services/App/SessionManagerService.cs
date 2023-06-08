// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public partial class SessionManagerService : ObservableObject, ISessionManagerService
{
    private readonly ILogger _logger;
    private readonly SessionFactory _sessionFactory;
    private readonly RoleRepository _roleRepository;
    private readonly EmployeeRoleRepository _employeeRoleRepository;

    [ObservableProperty]
    private Session? _sessionUser;

    public SessionManagerService(
        ILogger logger,
        SessionFactory sessionFactory,
        EmployeeRoleRepository employeeRoleRepository,
        RoleRepository roleRepository
    )
    {
        _logger = logger;
        _sessionFactory = sessionFactory;
        _roleRepository = roleRepository;
        _employeeRoleRepository = employeeRoleRepository;
    }

    public Session? Session { get; set; }

    public void StartSession(Employee account)
    {
        var employeeRole = _employeeRoleRepository.Find(
            employeeRole => employeeRole.EmployeeId == account.Id
        );
        var role = _roleRepository.Find(role => role.Id == employeeRole!.RoleId);
        SessionUser = _sessionFactory.Create(session =>
        {
            session.SessionUser = account;
            session.SessionRole = role!;
        });
        _logger.Log(this, $"Started user session");
    }

    public void EndSession()
    {
        SessionUser = null;
        _logger.Log(this, "Ended user session");
    }
}

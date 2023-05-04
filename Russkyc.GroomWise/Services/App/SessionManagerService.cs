// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public partial class SessionManagerService : ObservableObject, ISessionManagerService
{
    private readonly ILogger _logger;
    
    [ObservableProperty]
    private IAccount? _sessionUser;

    public SessionManagerService(ILogger logger)
    {
        _logger = logger;
    }

    public void StartSession(IAccount account)
    {
        SessionUser = account;
        _logger.Log(this,$"Started session for user {account.Username?.Substring(0,12)}");
    }

    public void EndSession()
    {
        SessionUser = null;
        _logger.Log(this,"Ended user session");
        
        BuilderServices.Resolve<ILoginView>()
            .ClearFields("Password");
        BuilderServices.Resolve<ILoginView>()
            .Show();
        BuilderServices.Resolve<IMainView>()
            .Hide();
    }

}
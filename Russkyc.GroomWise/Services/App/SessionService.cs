// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public class SessionService : ISessionService
{
    private IAccount? _account;

    public void Login(IAccount account)
    {
        _account = account;
    }

    public void Logout()
    {
        _account = null;
        BuilderServices.Resolve<LoginView>().ClearPasswords();
        BuilderServices.Resolve<LoginView>().Show();
        BuilderServices.Resolve<MainView>().Hide();
    }

    public IAccount? GetSession()
    {
        return _account;
    }
}
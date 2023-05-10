// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public class CredentialStore : ICredentialStore
{
    public void Save(string key, string value)
    {
        using var credential = new Credential
        {
            Target = key,
            Password = value,
            Type = CredentialType.Generic,
            PersistanceType = PersistanceType.LocalComputer
        };
        credential.Save();
    }

    public string Get(string key)
    {
        using var credential = new Credential { Target = key };
        credential.Load();
        return credential.Password;
    }
}
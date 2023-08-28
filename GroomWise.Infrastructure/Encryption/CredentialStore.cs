// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using CredentialManagement;
using Lombok.NET;

namespace GroomWise.Infrastructure.Encryption;

[Singleton]
public partial class CredentialStore
{
    public void Save(string key, string value)
    {
        using var credential = new Credential();
        credential.Target = key;
        credential.Password = value;
        credential.Type = CredentialType.Generic;
        credential.PersistanceType = PersistanceType.LocalComputer;
        credential.Save();
    }

    public string Get(string key)
    {
        using var credential = new Credential();
        credential.Target = key;
        credential.Load();
        return credential.Password;
    }
}

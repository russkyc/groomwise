// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

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

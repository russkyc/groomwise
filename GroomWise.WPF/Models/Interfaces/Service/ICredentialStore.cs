// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.Service;


/// <summary>
/// Secure storage wrapper for Microsoft Credential Manager
/// </summary>
public interface ICredentialStore
{
    
    /// <summary>
    /// Saves credential to secure storage
    /// </summary>
    /// <param name="key">credential id</param>
    /// <param name="value">credential to save</param>
    void Save(string key, string value);
    
    /// <summary>
    /// Gets credential from secure storage
    /// </summary>
    /// <param name="key">credential id</param>
    /// <returns>credential (string)</returns>
    string Get(string key);
}
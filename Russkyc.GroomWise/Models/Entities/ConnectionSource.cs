// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Entities;

public class ConnectionSource
{
    public string? Path { get; set; }
    public string? Port { get; set; }
    public string? Database { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public ConnectionSource WithPath(string path)
    {
        Path = path;
        return this;
    }

    public ConnectionSource WithPort(string port)
    {
        Port = port;
        return this;
    }

    public ConnectionSource WithDatabase(string database)
    {
        Database = database;
        return this;
    }

    public ConnectionSource WithUsername(string username)
    {
        Username = username;
        return this;
    }

    public ConnectionSource WithPassword(string password)
    {
        Password = password;
        return this;
    }
}

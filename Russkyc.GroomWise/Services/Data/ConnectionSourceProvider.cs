// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data;

public class ConnectionSourceProvider : IConnectionSourceProvider
{
    public string Build(IConnectionSource connectionSource, DbProvider provider)
    {
        return provider switch
        {
            DbProvider.LiteDb
                => $@"Filename={
                connectionSource.Path};Password={(string.IsNullOrEmpty(connectionSource.Password)
                    ? "" : $"Password={connectionSource.Password};")};",
            DbProvider.Sqlite
                => $@"Data Source={
                connectionSource.Path};{(string.IsNullOrEmpty(connectionSource.Password)
                    ? "" : $"Password={connectionSource.Password};")}",
            DbProvider.MySql
                => $@"Server={
                connectionSource.Path};Port={
                    connectionSource.Port};Database={
                        connectionSource.Database};Uid={
                            connectionSource.Username};Pwd={
                                connectionSource.Password};",
            _ => $"{connectionSource.Path}"
        };
    }
}

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

using GroomWise.Infrastructure.Storage.Interfaces;
using Injectio.Attributes;
using LiteDB;
using Russkyc.DependencyInjection.Attributes;
using Russkyc.DependencyInjection.Enums;

namespace GroomWise.Infrastructure.Storage;

[RegisterSingleton<IFileStorage, FileStorage>]
public class FileStorage : IFileStorage
{
    private readonly ILiteDatabase _db = new LiteDatabase("storage.db");

    public void Upload(string id, string path)
    {
        _db.FileStorage.Upload(id, path);
    }

    public Stream Get(string id, Stream stream)
    {
        _db.FileStorage.Download(id, stream);
        return stream;
    }

    public void Download(string id, string downloadPath, bool replace = false)
    {
        _db.FileStorage.Download(id, downloadPath, replace);
    }
}

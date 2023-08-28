// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

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

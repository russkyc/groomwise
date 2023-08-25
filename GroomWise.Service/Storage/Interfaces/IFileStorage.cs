// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Service.Storage.Interfaces;

public interface IFileStorage
{
    void Upload(string id, string path);
    Stream Get(string id, Stream stream);
    void Download(string id, string downloadPath, bool replace = false);
}
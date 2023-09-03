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

namespace GroomWise.Infrastructure.Storage.Interfaces;

public interface IFileStorage
{
    void Upload(string id, string path);
    Stream Get(string id, Stream stream);
    void Download(string id, string downloadPath, bool replace = false);
}
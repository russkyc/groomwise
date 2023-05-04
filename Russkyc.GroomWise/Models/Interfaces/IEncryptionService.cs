// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces;

public interface IEncryptionService
{
    T Encrypt<T>(T item);
    T Encrypt<T>(T item, params string[] ignore);
    T Decrypt<T>(T item);
    T Hash<T>(T item);
    T Hash<T>(T item, params string[] ignore);
}
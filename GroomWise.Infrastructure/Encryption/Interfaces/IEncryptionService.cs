// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Infrastructure.Encryption.Interfaces;

public interface IEncryptionService
{
    string Encrypt(string toEncrypt);
    T Encrypt<T>(T item, params string[] ignore);
    string Decrypt(string toDecrypt);
    T Decrypt<T>(T item, params string[] ignore);
    string Hash(string toDecrypt);
    T Hash<T>(T item, params string[] ignore);
}

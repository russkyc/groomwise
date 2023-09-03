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

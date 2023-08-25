// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.Service;

/// <summary>
/// Entity encryption service
/// </summary>
public interface IEncryptionService
{
    /// <summary>
    /// Encrypts a single string
    /// </summary>
    /// <param name="toEncrypt">String to encrypt</param>
    /// <returns></returns>
    string Encrypt(string toEncrypt);

    /// <summary>
    /// Encrypts all text fields within an object
    /// </summary>
    /// <param name="item">Item to encrypt</param>
    /// <typeparam name="T">Type of item to encrypt</typeparam>
    /// <returns>Item with encrypted fields</returns>
    T Encrypt<T>(T item);

    /// <summary>
    /// <inheritdoc cref="Encrypt{T}(T)"/>
    /// </summary>
    /// <param name="item">Item to encrypt</param>
    /// <param name="ignore">Fields that will not be encrypted</param>
    /// <typeparam name="T">Type of item to encrypt</typeparam>
    /// <returns>Item with encrypted fields</returns>
    T Encrypt<T>(T item, params string[] ignore);

    /// <summary>
    /// Decrypts a single string
    /// </summary>
    /// <param name="toDecrypt">String to decrypt</param>
    /// <returns></returns>
    string Decrypt(string toDecrypt);

    /// <summary>
    /// Decrypts all text fields within an object
    /// </summary>
    /// <param name="item">Item to decrypt</param>
    /// <typeparam name="T">Type of item to decrypt</typeparam>
    /// <returns>Item with decrypted fields</returns>
    T Decrypt<T>(T item);

    /// <summary>
    /// Hashes a single string
    /// </summary>
    /// <param name="item">String to hash</param>
    /// <returns></returns>
    string Hash(string item);

    /// <summary>
    /// Hashes all text fields within an object
    /// </summary>
    /// <param name="item">Item to hash</param>
    /// <typeparam name="T">Type of item to hash</typeparam>
    /// <returns>Item with hashed fields</returns>
    T Hash<T>(T item);

    /// <summary>
    /// <inheritdoc cref="Hash{T}(T)"/>
    /// </summary>
    /// <param name="item">Item to hash</param>
    /// <param name="ignore">Fields that will not be hashed</param>
    /// <typeparam name="T">Type of item to hash</typeparam>
    /// <returns></returns>
    T Hash<T>(T item, params string[] ignore);
}

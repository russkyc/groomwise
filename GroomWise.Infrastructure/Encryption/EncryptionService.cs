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

using System.Reflection;
using GroomWise.Infrastructure.Encryption.Interfaces;
using Injectio.Attributes;
using NETCore.Encrypt;
using NETCore.Encrypt.Extensions;
using Russkyc.DependencyInjection.Attributes;
using Russkyc.DependencyInjection.Enums;

namespace GroomWise.Infrastructure.Encryption;

[RegisterSingleton<IEncryptionService, EncryptionService>]
public class EncryptionService : IEncryptionService
{
    public EncryptionService()
    {
        // Does not contain key and iv
        if (
            CredentialStore.Instance.Get("AesKey") == String.Empty
            && CredentialStore.Instance.Get("AesIv") == String.Empty
        )
        {
            // Create new AES Key
            var aesKey = EncryptProvider.CreateAesKey();

            // Store AES Key and IV to secure storage
            CredentialStore.Instance.Save("AesKey", aesKey.Key);
            CredentialStore.Instance.Save("AesIv", aesKey.IV);
        }
    }

    public string Encrypt(string toEncrypt)
    {
        return EncryptProvider.AESEncrypt(
            toEncrypt,
            CredentialStore.Instance.Get("AesKey"),
            CredentialStore.Instance.Get("AesIv")
        );
    }

    public T Encrypt<T>(T item, params string[] ignore)
    {
        item?.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(field => field.FieldType == typeof(string))
            .ToList()
            .ForEach(fieldInfo =>
            {
                var currentValue = (string)fieldInfo.GetValue(item)!;
                if (
                    !string.IsNullOrEmpty(currentValue)
                    && !ignore.ToList().Any(fieldName => fieldInfo.Name.Contains(fieldName))
                )
                    fieldInfo.SetValue(
                        item,
                        string.IsNullOrEmpty(currentValue)
                            ? string.Empty
                            : EncryptProvider.AESEncrypt(
                                currentValue,
                                CredentialStore.Instance.Get("AesKey"),
                                CredentialStore.Instance.Get("AesIv")
                            )
                    );
            });
        return item;
    }

    public string Decrypt(string toDecrypt)
    {
        return EncryptProvider.AESDecrypt(
            toDecrypt,
            CredentialStore.Instance.Get("AesKey"),
            CredentialStore.Instance.Get("AesIv")
        );
    }

    public T Decrypt<T>(T item, params string[] ignore)
    {
        // Get fields in item
        item?.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            // where field is type(String)
            .Where(field => field.FieldType == typeof(string))
            .ToList()
            .ForEach(fieldInfo =>
            {
                var currentValue = (string)fieldInfo.GetValue(item)!;
                if (
                    !string.IsNullOrEmpty(currentValue)
                    && !ignore.ToList().Any(fieldName => fieldInfo.Name.Contains(fieldName))
                )
                {
                    fieldInfo.SetValue(
                        item,
                        string.IsNullOrEmpty(currentValue)
                            ? string.Empty
                            : EncryptProvider.AESDecrypt(
                                currentValue,
                                CredentialStore.Instance.Get("AesKey"),
                                CredentialStore.Instance.Get("AesIv")
                            )
                    );
                }
            });
        return item;
    }

    public string Hash(string toDecrypt)
    {
        return toDecrypt.SHA256();
    }

    public T Hash<T>(T item, params string[] ignore)
    {
        item?.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(field => field.FieldType == typeof(string))
            .ToList()
            .ForEach(f =>
            {
                var currentValue = (string)f.GetValue(item)!;
                if (!ignore.ToList().Any(fieldName => f.Name.Contains(fieldName)))
                    f.SetValue(
                        item,
                        string.IsNullOrEmpty(currentValue) ? string.Empty : currentValue.SHA256()
                    );
            });
        return item;
    }
}

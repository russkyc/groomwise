// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

/// <inheritdoc />
public class EncryptionService : IEncryptionService
{
    private readonly ICredentialStore _credentialStore;

    public EncryptionService(ICredentialStore credentialStore)
    {
        // Init secure storage
        _credentialStore = credentialStore;

        // Does not contain key and iv
        if (
            credentialStore.Get("AesKey") == String.Empty
            && credentialStore.Get("AesIv") == String.Empty
        )
        {
            // Create new AES Key
            var aesKey = EncryptProvider.CreateAesKey();

            // Store AES Key and IV to secure storage
            credentialStore.Save("AesKey", aesKey.Key);
            credentialStore.Save("AesIv", aesKey.IV);
        }
    }

    /// <inheritdoc />
    public string Encrypt(string toEncrypt)
    {
        return EncryptProvider.AESEncrypt(
            toEncrypt,
            _credentialStore.Get("AesKey"),
            _credentialStore.Get("AesIv")
        );
    }

    /// <inheritdoc />
    public T Encrypt<T>(T item)
    {
        // Get fields in item
        item?.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            // where field is type(String)
            .Where(field => field.FieldType == typeof(string))
            .ToList()
            .ForEach(f =>
            {
                var currentValue = (string)f.GetValue(item)!;
                f.SetValue(
                    item,
                    string.IsNullOrEmpty(currentValue)
                        ? string.Empty
                        : EncryptProvider.AESEncrypt(
                            currentValue,
                            _credentialStore.Get("AesKey"),
                            _credentialStore.Get("AesIv")
                        )
                );
            });
        return item;
    }

    /// <inheritdoc />
    public T Encrypt<T>(T item, params string[] ignore)
    {
        item?.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(field => field.FieldType == typeof(string))
            .ToList()
            .ForEach(f =>
            {
                var currentValue = (string)f.GetValue(item)!;
                if (
                    !string.IsNullOrEmpty(currentValue)
                    && !ignore.ToList().Any(fieldName => f.Name.Contains(fieldName))
                )
                    f.SetValue(
                        item,
                        string.IsNullOrEmpty(currentValue)
                            ? string.Empty
                            : EncryptProvider.AESEncrypt(
                                currentValue,
                                _credentialStore.Get("AesKey"),
                                _credentialStore.Get("AesIv")
                            )
                    );
            });
        return item;
    }

    /// <inheritdoc />
    public string Decrypt(string toDecrypt)
    {
        return EncryptProvider.AESDecrypt(
            toDecrypt,
            _credentialStore.Get("AesKey"),
            _credentialStore.Get("AesIv")
        );
    }

    /// <inheritdoc />
    public T Decrypt<T>(T item)
    {
        // Get fields in item
        item?.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            // where field is type(String)
            .Where(field => field.FieldType == typeof(string))
            .ToList()
            .ForEach(f =>
            {
                var currentValue = (string)f.GetValue(item)!;
                f.SetValue(
                    item,
                    string.IsNullOrEmpty(currentValue)
                        ? string.Empty
                        : EncryptProvider.AESDecrypt(
                            currentValue,
                            _credentialStore.Get("AesKey"),
                            _credentialStore.Get("AesIv")
                        )
                );
            });
        return item;
    }

    public string Hash(string item)
    {
        return item.SHA256();
    }

    /// <inheritdoc />
    public T Hash<T>(T item)
    {
        // Get fields in item
        item?.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            // where field is type(String)
            .Where(field => field.FieldType == typeof(string))
            .ToList()
            .ForEach(f =>
            {
                var currentValue = (string)f.GetValue(item)!;
                f.SetValue(
                    item,
                    string.IsNullOrEmpty(currentValue) ? string.Empty : currentValue.SHA256()
                );
            });
        return item;
    }

    /// <inheritdoc />
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

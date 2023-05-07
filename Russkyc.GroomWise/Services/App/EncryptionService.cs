// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public class EncryptionService : IEncryptionService
{

    private readonly string? _aesKey;
    private readonly string? _aesIv;
    
    public EncryptionService(ICredentialStore credentialStore)
    {
        
        // Check secure storage for key and iv
        var key = credentialStore.Get(nameof(_aesKey));
        var iv = credentialStore.Get(nameof(_aesIv));
        
        // Does not contain key and iv
        if (key == String.Empty && iv == String.Empty)
        {
            // Create new AES Key
            var aesKey = EncryptProvider.CreateAesKey();
            
            // Store AES Key and IV to secure storage
            credentialStore.Save(nameof(_aesKey), aesKey.Key);
            credentialStore.Save(nameof(_aesIv), aesKey.IV);
            
            // Set instance Key and IV to generated AES Key and IV
            _aesKey = aesKey.Key;
            _aesIv = aesKey.IV;
        }
        else
        {
            // Set instance Key and IV to secure storage Key and IV
            _aesKey = key;
            _aesIv = iv;
        }
    }
    
    public T Encrypt<T>(T item)
    {
        // Get fields in item
        item?.GetType()
            .GetFields(BindingFlags.Instance
                       | BindingFlags.Public
                       | BindingFlags.NonPublic)
            // where field is type(String)
            .Where(field => field
                .FieldType == typeof(string))
            .ToList()
            .ForEach(
                f =>
                {
                    var currentValue = (string)f.GetValue(item)!;
                    f.SetValue(item, string.IsNullOrEmpty(currentValue)
                        ? string.Empty : EncryptProvider.AESEncrypt(currentValue,_aesKey,_aesIv));
                });
        return item;
    }

    public T Encrypt<T>(T item, params string[] ignore)
    {
        item?.GetType()
            .GetFields(BindingFlags.Instance
                                  | BindingFlags.Public
                                  | BindingFlags.NonPublic)
            .Where(field => field.FieldType == typeof(string))
            .ToList()
            .ForEach(
                f =>
                {
                    var currentValue = (string)f.GetValue(item)!;
                    if (!string.IsNullOrEmpty(currentValue) &&
                        !ignore.ToList()
                            .Any(fieldName => f.Name
                                .Contains(fieldName)))
                        f.SetValue(item, string.IsNullOrEmpty(currentValue)
                        ? string.Empty : EncryptProvider.AESEncrypt(currentValue,_aesKey,_aesIv));
                });
        return item;
    }

    public T Decrypt<T>(T item)
    {
        // Get fields in item
        item?.GetType()
            .GetFields(BindingFlags.Instance
                       | BindingFlags.Public
                       | BindingFlags.NonPublic)
            // where field is type(String)
            .Where(field => field
                .FieldType == typeof(string))
            .ToList()
            .ForEach(
                f =>
                {
                    var currentValue = (string)f.GetValue(item)!;
                    f.SetValue(item, EncryptProvider.AESDecrypt(currentValue,_aesKey,_aesIv));
                });
        return item;
    }

    public T Hash<T>(T item)
    {
        // Get fields in item
        item?.GetType()
            .GetFields(BindingFlags.Instance
                                  | BindingFlags.Public
                                  | BindingFlags.NonPublic)
            // where field is type(String)
            .Where(field => field
                .FieldType == typeof(string))
            .ToList()
            .ForEach(
                f =>
                {
                    var currentValue = (string)f.GetValue(item)!;
                    f.SetValue(item, string.IsNullOrEmpty(currentValue)
                        ? string.Empty : currentValue.SHA256());
                });
        return item;
    }

    public T Hash<T>(T item, params string[] ignore)
    {
        item?.GetType()
            .GetFields(BindingFlags.Instance
                       | BindingFlags.Public
                       | BindingFlags.NonPublic)
            .Where(field => field.FieldType == typeof(string))
            .ToList()
            .ForEach(
                f =>
                {
                    var currentValue = (string)f.GetValue(item)!;
                    if (!ignore.ToList()
                            .Any(fieldName => f.Name
                                .Contains(fieldName)))
                        f.SetValue(item, string.IsNullOrEmpty(currentValue)
                            ? string.Empty : currentValue.SHA256());
                });
        return item;
    }
}
// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

#region

using System.Reflection;

#endregion

namespace GroomWise.Services.App;

public class EncryptionService : IEncryptionService
{
    public T Encrypt<T>(T item)
    {
        throw new NotImplementedException();
    }

    public T Encrypt<T>(T item, params string[] ignore)
    {
        throw new NotImplementedException();
    }

    public T Decrypt<T>(T item)
    {
        throw new NotImplementedException();
    }

    public T Hash<T>(T item)
    {
        item?.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(field => field.FieldType == typeof(string))
            .ToList()
            .ForEach(
                f =>
                {
                    string currentValue = (string)f.GetValue(item)!;
                    if (!string.IsNullOrEmpty(currentValue))
                    {
                        f.SetValue(item, currentValue.SHA256());
                    }
                });
        return item;
    }

    public T Hash<T>(T item, params string[] ignore)
    {
        item?.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(field => field.FieldType == typeof(string))
            .ToList()
            .ForEach(
                f =>
                {
                    string currentValue = (string)f.GetValue(item)!;
                    if (!string.IsNullOrEmpty(currentValue) &&
                        !ignore.ToList().Any(fieldName => f.Name.Contains(fieldName)))
                    {
                        f.SetValue(item, currentValue.SHA256());
                    }
                });
        return item;
    }
}
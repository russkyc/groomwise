// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using JsonFlatFileDataStore;

namespace Russkyc.Groomwise.Cli.Services;

public class Migration<T>
    where T : class
{
    private const string MIGRATIONS_PATH = "Migrations";
    private readonly string _path;
    private readonly string _name;
    private List<T> _collection;

    public Migration(string path, string name, List<T> collection)
    {
        _collection = collection;
        _name = name;
        _path = path;

        if (!Directory.Exists(MIGRATIONS_PATH))
            Directory.CreateDirectory(MIGRATIONS_PATH);
    }

    public void Create()
    {
        var store = new DataStore($"{MIGRATIONS_PATH}/{_path}");
        store.GetCollection<T>(_name).InsertMany(_collection.AsEnumerable());
    }
}

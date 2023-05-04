// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class LiteDatabaseFactory : ILiteDatabaseFactory
{
    
    public ILiteDatabase Create()
    {
        throw new NotImplementedException();
    }

    public ILiteDatabase Create(params object[] values)
    {
        return new LiteDatabase((string)values[0]);
    }
}
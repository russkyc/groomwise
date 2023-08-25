// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class CustomerAddressRepository : Repository<CustomerAddress>
{
    public CustomerAddressRepository(IDatabaseServiceAsync databaseService)
        : base(databaseService) { }
}

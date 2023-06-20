// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class AddCustomersViewFactory : IFactory<AddCustomersView>
{
    public AddCustomersView Create(Action<AddCustomersView> builder = null)
    {
        var addCustomersView = ((AddCustomersView)BuilderServices.Resolve<IAddCustomersView>())
            .AsChild()
            .HideOnParentMouseDown();
        if (builder != null)
            builder(addCustomersView);
        return addCustomersView;
    }
}

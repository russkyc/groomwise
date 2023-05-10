// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class AddAppointmentsViewFactory : IAddAppointmentsViewFactory
{
    public AddAppointmentsView Create()
    {
        return ((AddAppointmentsView)BuilderServices.Resolve<IAddAppointmentsView>())
            .AsChild()
            .HideOnParentMouseDown();
    }

    public AddAppointmentsView Create(params object[] values)
    {
        return ((AddAppointmentsView)BuilderServices.Resolve<IAddAppointmentsView>())
            .AddParent((ModernWindow)values[0])
            .HideOnParentMouseDown();
    }
}
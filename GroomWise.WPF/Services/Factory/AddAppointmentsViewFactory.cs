// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class AddAppointmentsViewFactory : IFactory<AddAppointmentsView>
{
    public AddAppointmentsView Create(Action<AddAppointmentsView>? builder = null)
    {
        var addAppointmentsView = (
            (AddAppointmentsView)BuilderServices.Resolve<IAddAppointmentsView>()
        )
            .AsChild()
            .HideOnParentMouseDown();
        if (builder != null)
            builder(addAppointmentsView);
        return addAppointmentsView;
    }
}

// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class AddAppointmentsViewFactory : IAddAppointmentsViewFactory
{
    public AddAppointmentsView Create(Action<AddAppointmentsView> builder = null)
    {
        var appointmentsView = (
            (AddAppointmentsView)BuilderServices.Resolve<IAddAppointmentsView>()
        )
            .AsChild()
            .HideOnParentMouseDown();
        builder(appointmentsView);
        return new AddAppointmentsView();
    }
}

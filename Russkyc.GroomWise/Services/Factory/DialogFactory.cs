// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using Russkyc.Abstractions.Abstractions;

namespace GroomWise.Services.Factory;

public class DialogFactory : Factory<DialogView>
{
    public new DialogView Create(Action<DialogView>? builder = null)
    {
        var dialogView = new DialogView().AsChild();
        if (builder != null)
            builder(dialogView);
        return dialogView;
    }
}

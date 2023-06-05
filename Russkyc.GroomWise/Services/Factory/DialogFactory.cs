// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class DialogFactory : IDialogFactory
{
    public DialogView Create(Action<DialogView>? builder = null)
    {
        var dialogView = new DialogView().AsChild();
        builder!(dialogView);
        return dialogView;
    }
}

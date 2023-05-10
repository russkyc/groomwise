// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class DialogFactory : IDialogFactory
{
    public DialogView Create()
    {
        return new DialogView("Are you sure?", "")
            .AddParent((ModernWindow)BuilderServices.Resolve<IMainView>())
            .ExitOnParentMouseDown();
    }

    public DialogView Create(params object[] values)
    {
        return values.Length == 2
            ? new DialogView((string)values[0], (string)values[1])
                .AddParent((ModernWindow)BuilderServices.Resolve<IMainView>())
                .ExitOnParentMouseDown()
            : new DialogView((string)values[0], (string)values[1], (MessageBoxButton)values[2])
                .AddParent((ModernWindow)BuilderServices.Resolve<IMainView>())
                .ExitOnParentMouseDown();
    }
}

public class MetroWindow { }

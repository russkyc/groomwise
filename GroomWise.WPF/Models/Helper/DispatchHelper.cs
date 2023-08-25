// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Windows.Threading;

namespace GroomWise.Models.Helper;

public static class DispatchHelper
{
    public static DispatcherOperation UiInvoke(this Action action)
    {
        return Application.Current.Dispatcher.BeginInvoke(action);
    }

    public static DispatcherOperation UiInvokeAsync(this Action action)
    {
        return Application.Current.Dispatcher.InvokeAsync(action);
    }
}

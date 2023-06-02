// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Helper;

public static class WindowHelper
{
    public static T AsChild<T>(this T child)
        where T : Window
    {
        child.Owner = (ModernWindow)BuilderServices.Resolve<IMainView>();
        child.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        child.ShowInTaskbar = false;
        return child;
    }

    public static T AddParent<T>(this T child, Window parent)
        where T : Window
    {
        child.Owner = parent;
        child.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        child.ShowInTaskbar = false;
        return child;
    }

    public static T ExitOnParentMouseDown<T>(this T child)
        where T : Window
    {
        child.Owner.MouseDown += (s, e) => child.Close();
        child.Owner.PreviewMouseDown += (s, e) => child.Close();
        return child;
    }

    public static T HideOnParentMouseDown<T>(this T child)
        where T : Window
    {
        child.Owner.MouseDown += (s, e) => child.Hide();
        child.Owner.PreviewMouseDown += (s, e) => child.Hide();
        return child;
    }
}

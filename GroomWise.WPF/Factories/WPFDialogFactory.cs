// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Threading.Tasks;
using System.Windows;
using GroomWise.Domain.Interfaces;
using GroomWise.Infrastructure.Navigation;
using GroomWise.Views.Dialogs;

namespace GroomWise.Factories;

public class WPFDialogFactory : IDialogFactory
{
    public bool? Create(string messageBoxText, string caption)
    {
        return Task.Run(async () =>
        {
            return await App.Current.Dispatcher.InvokeAsync(() =>
            {
                var dialog = new DialogView(messageBoxText, caption);
                dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                dialog.Owner = (Window)NavigationService.Instance?.CurrentWindow!;
                return dialog.ShowDialog();
            });
        }).Result;
    }
}

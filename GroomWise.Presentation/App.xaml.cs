using System;
using GroomWise.Presentation.Views;
using org.russkyc.moderncontrols.Helpers;
using Russkyc.GroomWise.Desktop.ViewModels;

namespace Russkyc.GroomWise.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    /// <summary>
    /// Application Entry for GroomWise.Presentation
    /// </summary>
    public App()
    {
        InitializeComponent();
        ThemeManager.Instance.SetBaseTheme("Light");
        
        var view = new MainView
        {
            DataContext = Activator.CreateInstance<MainViewModel>()
        };

        view.Show();
    }
}
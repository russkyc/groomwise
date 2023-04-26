using System;
using GroomWise.Presentation.ViewModels;
using GroomWise.Presentation.Views;

namespace GroomWise.Presentation;

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
        var view = new MainView
        {
            DataContext = Activator.CreateInstance<MainViewModel>()
        };

        view.Show();
    }
}
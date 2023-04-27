using CommunityToolkit.Mvvm.ComponentModel;

namespace Russkyc.GroomWise.Desktop.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string? _welcomeMessage;

    public MainViewModel()
    {
        WelcomeMessage = "Welcome to your MVVM App!";
    }
}
// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class LoginViewModel : ViewModelBase, ILoginViewModel
{
    private readonly IAccountsRepositoryService _accountsRepositoryService;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Password cannot be blank.")]
    private string _password;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Username cannot be blank.")]
    [RegularExpression("^[a-zA-Z0-9_-]{1,16}$", ErrorMessage = "Invalid Username format.")]
    private string _username;

    public LoginViewModel(IAccountsRepositoryService accountsRepositoryService)
    {
        _accountsRepositoryService = accountsRepositoryService;
    }

    [RelayCommand]
    private void SwitchFocus(object element)
    {
        element.GetType()
            .GetMethod("Focus")?
            .Invoke(element, null);
    }

    [RelayCommand]
    private void Login()
    {
        if (_accountsRepositoryService.Get(account => account.Username == Username && account.Password == Password) !=
            null)
        {
            BuilderServices.Resolve<MainView>().Show();
            BuilderServices.Resolve<LoginView>().Hide();
        }
        else
        {
            BuilderServices.Resolve<LoginView>().ClearFields();
        }
    }
}
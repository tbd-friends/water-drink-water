using blazor.wa.tbd.Services;
using Microsoft.AspNetCore.Components;

namespace blazor.wa.tbd.Components;

public partial class Login
{
    private readonly LoginFormData formData = new();

    [Inject] public UserService UserService { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    public bool ShowAuthError { get; set; }
    public string Error { get; set; }

    public async Task ExecuteLogin()
    {
        ShowAuthError = false;

        var result = await UserService.Authenticate(formData.Email, formData.Password);
        if (!result)
        {
            Error = "Invalid email or password";
            ShowAuthError = true;
        }
        else
        {
            NavigationManager.NavigateTo("/");
        }
    }

    public class LoginFormData
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
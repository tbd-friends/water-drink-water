using blazor.wa.tbd.Infrastructure;
using blazor.wa.tbd.Services;
using Microsoft.AspNetCore.Components;

namespace blazor.wa.tbd.Pages;

public partial class Home
{
    [Inject] private UserService UserService { get; set; } = null!;
    [Inject] private NavigationManager Navigation { get; set; } = null!;
    [Inject] private CustomAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    private string Email { get; set; } = "";
    private string Password { get; set; } = "";

    protected override async Task OnInitializedAsync()
    {
        if (await UserService.IsAuthenticated())
        {
            Navigation.NavigateTo("/log");
        }
    }

    public async Task Login()
    {
        if (await UserService.Authenticate(Email, Password))
        {
            AuthenticationStateProvider.NotifyUserHasChanged();

            StateHasChanged();

            Navigation.NavigateTo("/log");
        }
    }
}
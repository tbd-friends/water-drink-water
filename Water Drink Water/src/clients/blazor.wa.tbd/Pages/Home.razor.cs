using blazor.wa.tbd.Services;
using Microsoft.AspNetCore.Components;

namespace blazor.wa.tbd.Pages;

public partial class Home
{
    [Inject] private AuthService AuthService { get; set; } = null!;
    [Inject] private NavigationManager Navigation { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        if (await AuthService.IsAuthenticated())
        {
            Navigation.NavigateTo("/log");
        }
    }
}
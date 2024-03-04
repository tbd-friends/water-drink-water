using blazor.wa.tbd.Services;
using Microsoft.AspNetCore.Components;

namespace blazor.wa.tbd.Pages;

public partial class Home
{
    [Inject] private UserService UserService { get; set; } = null!;
    [Inject] private NavigationManager Navigation { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        if (await UserService.IsAuthenticated())
        {
            Navigation.NavigateTo("/log");
        }
    }
}
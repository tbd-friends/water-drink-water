using blazor.wa.tbd.Services;
using Microsoft.AspNetCore.Components;

namespace blazor.wa.tbd.Pages;

public partial class Preferences
{
    [Inject] private UserService UserService { get; set; } = null!;
    private int TargetFluidOunces { get; set; } = 0;

    private async Task SavePreferences()
    {
        await UserService.SavePreferences(TargetFluidOunces);
    }
}
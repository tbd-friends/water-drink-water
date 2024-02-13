using blazor.wa.tbd.Services;
using Microsoft.AspNetCore.Components;
using viewmodels;

namespace blazor.wa.tbd.Pages;

public partial class Preferences
{
    [Inject] private UserService UserService { get; set; } = null!;
    private PreferencesViewModel Current { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Current = await UserService.GetPreferences() ?? new PreferencesViewModel();
    }

    private async Task SavePreferences()
    {
        await UserService.SavePreferences(Current.TargetFluidOunces, Current.TimeZoneOffsetHours);
    }
}
using blazor.wa.tbd.Components;
using blazor.wa.tbd.Services;
using Microsoft.AspNetCore.Components;
using viewmodels;

namespace blazor.wa.tbd.Pages;

public partial class Preferences
{
    [CascadingParameter] private ConsumptionStateProvider ConsumptionStateProvider { get; set; } = null!;
    [Inject] private UserService UserService { get; set; } = null!;
    private PreferencesViewModel Current { get; set; } = new();
    private IEnumerable<UserService.TimeZoneModel> TimeZones { get; set; } = new List<UserService.TimeZoneModel>();

    protected override async Task OnInitializedAsync()
    {
        Current = await UserService.GetPreferences() ?? new PreferencesViewModel();
        TimeZones = await UserService.GetTimeZones();
    }

    private async Task SavePreferences()
    {
        await UserService.SavePreferences(Current.TargetFluidOunces, Current.TimeZoneId);

        await ConsumptionStateProvider.RefreshContext();
    }
}
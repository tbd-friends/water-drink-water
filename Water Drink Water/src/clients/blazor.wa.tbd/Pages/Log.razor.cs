using blazor.wa.tbd.Services;
using Microsoft.AspNetCore.Components;

namespace blazor.wa.tbd.Pages;

public partial class Log
{
    [Inject] private UserService UserService { get; set; } = null!;
    private int FluidOuncesConsumed { get; set; } = 0;

    private async Task LogConsumption()
    {
        await UserService.LogConsumption(FluidOuncesConsumed);
    }
}
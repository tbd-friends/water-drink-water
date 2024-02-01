using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using TbdFriends.WaterDrinkWater.Application.Services;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.Preferences;

public class Get(ConsumptionService service) : EndpointWithoutRequest<Results<Ok<PreferencesVm>, BadRequest>>
{
    public override void Configure()
    {
        Get("api/preferences");
    }

    public override Task<Results<Ok<PreferencesVm>, BadRequest>> ExecuteAsync(CancellationToken ct)
    {
        if (int.TryParse(
                User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.Split("|")[1],
                out var userId))
        {
            var preferences = service.GetPreferences(userId);
            return Task.FromResult<Results<Ok<PreferencesVm>, BadRequest>>(TypedResults.Ok(new PreferencesVm
            {
                TargetFluidOunces = preferences
            }));
        }

        return Task.FromResult<Results<Ok<PreferencesVm>, BadRequest>>(TypedResults.BadRequest());
    }
}

public class PreferencesVm
{
    public int? TargetFluidOunces { get; init; }
}
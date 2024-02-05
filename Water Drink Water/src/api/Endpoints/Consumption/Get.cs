using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using TbdFriends.WaterDrinkWater.Application.Services;
using viewmodels;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.Consumption;

public class Get(ConsumptionService service) : EndpointWithoutRequest<Results<Ok<PreferencesViewModel>, BadRequest>>
{
    public override void Configure()
    {
        Get("api/preferences");
    }

    public override Task<Results<Ok<PreferencesViewModel>, BadRequest>> ExecuteAsync(CancellationToken ct)
    {
        if (int.TryParse(
                User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.Split("|")[1],
                out var userId))
        {
            var preferences = service.GetPreferences(userId);
            
            return Task.FromResult<Results<Ok<PreferencesViewModel>, BadRequest>>(TypedResults.Ok(preferences));
        }

        return Task.FromResult<Results<Ok<PreferencesViewModel>, BadRequest>>(TypedResults.BadRequest());
    }
}
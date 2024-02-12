using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using TbdFriends.WaterDrinkWater.Application.Services;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.Consumption;

public class Get(ConsumptionService service) : EndpointWithoutRequest<Results<Ok<int>, BadRequest>>
{
    public override void Configure()
    {
        Get("api/consumption");
    }

    public override Task<Results<Ok<int>, BadRequest>> ExecuteAsync(CancellationToken ct)
    {
        if (int.TryParse(
                User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.Split("|")[1],
                out int userId))
        {
            var progress = service.GetProgressPercentage(userId);

            return Task.FromResult<Results<Ok<int>, BadRequest>>(TypedResults.Ok(progress));
        }

        return Task.FromResult<Results<Ok<int>, BadRequest>>(TypedResults.BadRequest());
    }
}
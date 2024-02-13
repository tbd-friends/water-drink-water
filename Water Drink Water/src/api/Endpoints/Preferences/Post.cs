using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using TbdFriends.WaterDrinkWater.Application.Services;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.Preferences;

public class Post(ConsumptionService service) : Endpoint<Post.Parameters, Results<Ok<bool>, BadRequest>>
{
    public override void Configure()
    {
        Post("api/preferences");
    }

    public override Task<Results<Ok<bool>, BadRequest>> ExecuteAsync(Parameters req, CancellationToken ct)
    {
        if (int.TryParse(
                User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.Split("|")[1],
                out int userId))
        {
            service.SetPreferences(
                userId,
                req.TargetFluidOunces,
                req.TimeZoneOffsetHours
            );

            return Task.FromResult<Results<Ok<bool>, BadRequest>>(TypedResults.Ok(true));
        }

        return Task.FromResult<Results<Ok<bool>, BadRequest>>(TypedResults.BadRequest());
    }

    public class Parameters
    {
        public int TargetFluidOunces { get; set; }
        public int TimeZoneOffsetHours { get; set; }
    }
}
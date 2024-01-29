using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using TbdFriends.WaterDrinkWater.Application.Services;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.Consumption;

public class Post(ConsumptionService service) : Endpoint<Post.Parameters, Results<Ok<bool>, BadRequest>>
{
    public override void Configure()
    {
        Post("api/consumption");
    }

    public override Task<Results<Ok<bool>, BadRequest>> ExecuteAsync(Parameters req, CancellationToken ct)
    {
        var user = User.Identity!.Name;

        if (int.TryParse(
                User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.Split("|")[1],
                out int userId))
        {
            service.Log(
                userId,
                req.FluidOuncesConsumed
            );

            return Task.FromResult<Results<Ok<bool>, BadRequest>>(TypedResults.Ok(true));
        }

        return Task.FromResult<Results<Ok<bool>, BadRequest>>(TypedResults.BadRequest());
    }

    public class Parameters
    {
        public int FluidOuncesConsumed { get; set; }
    }
}
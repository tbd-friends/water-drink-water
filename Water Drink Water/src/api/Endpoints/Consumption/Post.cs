using System.Diagnostics;
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
        var userId = User.Identity.Name;

        // service.Log(
        //     User.Identity.Name,
        //     req.FluidOuncesConsumed
        // );

        return base.ExecuteAsync(req, ct);
    }

    public class Parameters
    {
        public int FluidOuncesConsumed { get; set; }
    }
}
using System.Diagnostics;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.Consumption;

public class Post() : Endpoint<Post.Parameters, Results<Ok<bool>, BadRequest>>
{
    public override void Configure()
    {
        Post("api/consumption");
    }

    public override Task<Results<Ok<bool>, BadRequest>> ExecuteAsync(Parameters req, CancellationToken ct)
    {
        Debugger.Break();

        /// Service.LogConsumption(new Consumption {
        ///     UserId = User.Identify.Id,
        ///     FluidOuncesConsumed = req.FluidOuncesConsumed
        /// }));

        return base.ExecuteAsync(req, ct);
    }

    public class Parameters
    {
        public int FluidOuncesConsumed { get; set; }
    }
}
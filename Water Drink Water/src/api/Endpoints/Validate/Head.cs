using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.Validate;

public class Head : EndpointWithoutRequest<Results<Ok, BadRequest>>
{
    public override void Configure()
    {
        Head("api/validate");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Console.WriteLine("Handling HEAD request");

        await Task.CompletedTask;
    }
}
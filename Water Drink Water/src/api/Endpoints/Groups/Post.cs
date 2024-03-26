using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using TbdFriends.WaterDrinkWater.Api.Infrastructure;
using TbdFriends.WaterDrinkWater.Application.Services;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.Groups;

public class Post(GroupService groupService) : Endpoint<Post.Parameters, Results<Ok, BadRequest>>
{
    public override void Configure()
    {
        Post("api/groups");
    }

    public override Task<Results<Ok, BadRequest>> ExecuteAsync(Parameters parameters, CancellationToken ct)
    {
        if (User.GetUserId() is not (var userId and > 0))
        {
            return Task.FromResult<Results<Ok, BadRequest>>(TypedResults.BadRequest());
        }

        var result = groupService.AddNewGroup(parameters.Name, userId);

        return Task.FromResult<Results<Ok, BadRequest>>(result.IsSuccess
            ? TypedResults.Ok()
            : TypedResults.BadRequest());
    }

    public class Parameters
    {
        public string Name { get; set; } = null!;
    }
}
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using TbdFriends.WaterDrinkWater.Application.Services;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.Registration;

public class Post : Endpoint<Post.Parameters, Results<Ok<bool>, BadRequest>>
{
    private readonly AccountService _accountService;

    public Post(AccountService accountService)
    {
        _accountService = accountService;
    }

    public override void Configure()
    {
        Post("api/registration");

        AllowAnonymous();
    }

    public override Task HandleAsync(Parameters request, CancellationToken ct)
    {
        var result = _accountService.Register(
            request.Name,
            request.Email,
            request.Password);

        return Task.FromResult(result.IsSuccess ? Results.Ok() : Results.BadRequest());
    }

    public class Parameters
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
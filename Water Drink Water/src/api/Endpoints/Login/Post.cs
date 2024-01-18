using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using TbdFriends.WaterDrinkWater.Application.Services;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.Login;

public class Post : Endpoint<Post.Parameters, Results<Ok<string>, BadRequest>>
{
    private readonly LoginService _loginService;

    public Post(LoginService loginService)
    {
        _loginService = loginService;
    }

    public override void Configure()
    {
        Post("api/login");
        AllowAnonymous();
    }

    public override Task<Results<Ok<string>, BadRequest>> ExecuteAsync(Parameters req, CancellationToken ct)
    {
        var result = _loginService.Login(req.Email, req.Password);

        return !result.IsSuccess ? 
            Task.FromResult<Results<Ok<string>, BadRequest>>(TypedResults.BadRequest()) : 
            Task.FromResult<Results<Ok<string>, BadRequest>>(TypedResults.Ok(result.Value));
    }

    public class Parameters
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
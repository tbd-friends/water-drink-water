using FastEndpoints;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.Registration;

public class Post : Endpoint<Post.Parameters>
{
    public Post()
    {
        Post("api/registration");
    }

    public override Task HandleAsync(Parameters req, CancellationToken ct)
    {
        return Task.CompletedTask;
    }

    public class Parameters
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
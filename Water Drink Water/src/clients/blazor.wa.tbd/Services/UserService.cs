using System.Net.Http.Json;

namespace blazor.wa.tbd.Services;

public class UserService(HttpClient client)
{
    public async Task<string> Authenticate(string email, string password)
    {
        var response = await client.PostAsJsonAsync("api/login", new { email, password });
        var token = await response.Content.ReadAsStringAsync();

        if (token is null)
        {
            throw new("Invalid email or password");
        }

        return token;
    }
}
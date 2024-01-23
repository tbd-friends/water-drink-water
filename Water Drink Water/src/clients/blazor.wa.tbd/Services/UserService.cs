using System.Net.Http.Json;
using Blazored.LocalStorage;

namespace blazor.wa.tbd.Services;

public class UserService(
    HttpClient client,
    ILocalStorageService localStorage)
{
    public async Task<bool> IsAuthenticated()
    {
        var token = await localStorage.GetItemAsync<string>("token");

        return token is not null;
    }

    public async Task<bool> Authenticate(string email, string password)
    {
        var response = await client.PostAsJsonAsync("api/login", new { email, password });
        var token = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        await localStorage.SetItemAsync("token", token);

        return true;
    }
}
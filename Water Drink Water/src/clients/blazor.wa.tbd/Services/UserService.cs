using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;

namespace blazor.wa.tbd.Services;

public class UserService(
    HttpClient client,
    ILocalStorageService localStorage)
{
    private Lazy<ValueTask<string>> _token = new(localStorage.GetItemAsync<string>("token"));

    public async Task<bool> IsAuthenticated()
    {
        var token = await _token.Value;

        return !string.IsNullOrWhiteSpace(token);
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

    public async Task<bool> LogConsumption(int fluidOuncesConsumed)
    {
        var token = await _token.Value;

        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("api/consumption",
            new { fluidOuncesConsumed });

        return response.IsSuccessStatusCode;
    }
}
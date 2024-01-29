using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
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

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var content = await response.Content.ReadAsStringAsync();

        var loginResponse = JsonSerializer.Deserialize<LoginResponse>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (loginResponse is null)
        {
            return false;
        }

        await localStorage.SetItemAsync("token", loginResponse.Token);

        return true;
    }

    public async Task Logout()
    {
        await localStorage.ClearAsync();
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

    public class LoginResponse
    {
        public string? Token { get; set; }
    }
}
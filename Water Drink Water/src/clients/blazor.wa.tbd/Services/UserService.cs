using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using blazor.wa.tbd.Infrastructure;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using viewmodels;

namespace blazor.wa.tbd.Services;

public class UserService(
    HttpClient client,
    AuthenticationStateProvider authenticationStateProvider,
    ILocalStorageService localStorage)
{
    private readonly Lazy<ValueTask<string>> _token = new(localStorage.GetItemAsync<string>("token"));

    public async Task<bool> IsAuthenticated()
    {
        var token = await _token.Value;

        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        var request = new HttpRequestMessage(HttpMethod.Head, "api/validate");

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await client.SendAsync(request);

        return response.IsSuccessStatusCode;
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

        await localStorage.SetItemAsync("authToken", loginResponse.Token);
        ((CustomAuthenticationStateProvider)authenticationStateProvider).NotifyUserAuthentication(email);


        return true;
    }

    public async Task Logout()
    {
        await localStorage.RemoveItemAsync("authToken");
        ((CustomAuthenticationStateProvider)authenticationStateProvider).NotifyUserLogout();
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

    public async Task<PreferencesViewModel?> GetPreferences()
    {
        var token = await _token.Value;

        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await client.GetFromJsonAsync<PreferencesViewModel>("api/preferences");
    }

    public async Task<IEnumerable<TimeZoneModel>?> GetTimeZones()
    {
        return await client.GetFromJsonAsync<IEnumerable<TimeZoneModel>>("api/timezones");
    }

    public async Task<bool> SavePreferences(int targetFluidOunces, string timeZoneId)
    {
        var token = await _token.Value;

        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("api/preferences",
            new { targetFluidOunces, timeZoneId });

        return response.IsSuccessStatusCode;
    }

    public class LoginResponse
    {
        public string? Token { get; set; }
    }

    public class TimeZoneModel
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
    }
}
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using viewmodels;

namespace blazor.wa.tbd.Services;

public class UserService(
    HttpClient client,
    AuthenticationStateProvider authenticationStateProvider,
    ILocalStorageService localStorage)
{
    private readonly Lazy<ValueTask<string>> _token = new(value: localStorage.GetItemAsync<string>("token"));
    private ValueTask<string> Token => _token.Value;

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

    public async Task<int> GetConsumptionPercentage()
    {
        var token = await _token.Value;

        if (string.IsNullOrWhiteSpace(token))
        {
            return 0;
        }
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("api/consumption");

        if (!response.IsSuccessStatusCode)
        {
            return 0;
        }
        
        return await response.Content.ReadFromJsonAsync<int>();
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
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using blazor.wa.tbd.Infrastructure;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace blazor.wa.tbd.Services;

public class AuthService(
    HttpClient client,
    ILocalStorageService localStorageService,
    AuthenticationStateProvider authenticationStateProvider)
{
    public async Task Logout()
    {
        await localStorageService.RemoveItemAsync("token");

        ((CustomAuthenticationStateProvider)authenticationStateProvider).NotifyUserHasChanged();
    }

    public async Task<bool> IsAuthenticated()
    {
        var token = await localStorageService.GetItemAsync<string>("token");

        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Head, "api/validate");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                await localStorageService.RemoveItemAsync("token");
            }

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException _)
        {
            await localStorageService.RemoveItemAsync("token");

            return false;
        }
        finally
        {
            ((CustomAuthenticationStateProvider)authenticationStateProvider).NotifyUserHasChanged();
        }
    }

    public async Task<bool> Authenticate(string email, string password)
    {
        var response = await client.PostAsJsonAsync("api/login", new { email, password });

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var content = await response.Content.ReadAsStringAsync();

        var loginResponse = JsonSerializer.Deserialize<UserService.LoginResponse>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (loginResponse is null)
        {
            return false;
        }

        await localStorageService.SetItemAsync("token", loginResponse.Token);

        ((CustomAuthenticationStateProvider)authenticationStateProvider).NotifyUserHasChanged();

        return true;
    }
}
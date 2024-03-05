using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace blazor.wa.tbd.Infrastructure;

public class CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
    : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorageService.GetItemAsync<string>("token");

        var identity = !string.IsNullOrEmpty(token)
            ? new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") },
                authenticationType: nameof(CustomAuthenticationStateProvider))
            : new ClaimsIdentity();

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyUserHasChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
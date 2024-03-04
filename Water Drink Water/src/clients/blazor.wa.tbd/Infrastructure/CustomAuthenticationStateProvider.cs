using System.Security.Claims;
using blazor.wa.tbd.Helpers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace blazor.wa.tbd.Infrastructure;

public class CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
    : AuthenticationStateProvider
{
    private readonly AuthenticationState _anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorageService.GetItemAsync<string>("authToken");
        if (string.IsNullOrEmpty(token))
            return _anonymous;
        
        return new AuthenticationState(
            new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
    }

    public void NotifyUserAuthentication(string email)
    {
        var authenticatedUser =
            new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "jwtAuthType"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}
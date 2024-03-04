using System.Security.Claims;
using blazor.wa.tbd.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace blazor.wa.tbd.Infrastructure;

public class CustomAuthenticationStateProvider(UserService userService) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var isAuthenticated = await userService.IsAuthenticated();

        var identity = isAuthenticated
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
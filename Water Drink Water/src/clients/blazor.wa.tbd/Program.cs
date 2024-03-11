using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazor.wa.tbd;
using blazor.wa.tbd.Infrastructure;
using blazor.wa.tbd.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<UserService>(client => { client.BaseAddress = new Uri("https://localhost:7245"); });
builder.Services.AddHttpClient<AuthService>(client => { client.BaseAddress = new Uri("https://localhost:7245"); });

builder.Services.AddBlazoredLocalStorageAsSingleton();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddFluentUIComponents();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

await builder.Build().RunAsync();
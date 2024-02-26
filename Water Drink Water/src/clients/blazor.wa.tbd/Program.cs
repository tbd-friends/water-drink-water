using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazor.wa.tbd;
using blazor.wa.tbd.Services;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<UserService>(client => { client.BaseAddress = new Uri("https://localhost:7245"); });

builder.Services.AddBlazoredLocalStorageAsSingleton();

await builder.Build().RunAsync();
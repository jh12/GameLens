using GameLens.Client.Services;
using GameLens.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

IServiceCollection services = builder.Services;
services.AddMudServices();
services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

services.AddScoped<ICounterService, HttpCounterService>();
services.AddScoped<IHeroService, HttpHeroService>();

await builder.Build().RunAsync();

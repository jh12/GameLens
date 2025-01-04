using GameLens.Endpoints;
using GameLens.Services;
using GameLens.Shared.Services;
using GameLens.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

IServiceCollection services = builder.Services;

services.AddMudServices();
services.AddTransient<ICounterService, CounterService>();
services.AddTransient<IHeroService, HeroService>();

services.AddSingleton(builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GameLens.Client._Imports).Assembly);

app
    .MapCountersEndpoints()
    .MapHeroesEndpoints();

app.Run();

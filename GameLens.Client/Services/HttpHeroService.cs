using System.Net.Http.Json;
using GameLens.Shared.Models;
using GameLens.Shared.Services;

namespace GameLens.Client.Services;

internal class HttpHeroService : IHeroService
{
    private readonly HttpClient _client;

    public HttpHeroService(HttpClient client)
    {
        _client = client;
    }

    public IAsyncEnumerable<Hero> GetHeroes(CancellationToken cancellationToken = default)
    {
        return _client.GetFromJsonAsAsyncEnumerable<Hero>("api/hero", cancellationToken)!;
    }
}

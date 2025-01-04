using System.Net.Http.Json;
using GameLens.Shared.Models;
using GameLens.Shared.Services;

namespace GameLens.Client.Services;

internal class HttpCounterService : ICounterService
{
    private readonly HttpClient _client;

    public HttpCounterService(HttpClient client)
    {
        _client = client;
    }

    public IAsyncEnumerable<HeroCounter> GetCounters(CancellationToken cancellationToken = default)
    {
        return _client.GetFromJsonAsAsyncEnumerable<HeroCounter>("api/counters", cancellationToken)!;
    }
}

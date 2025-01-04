using System.Runtime.CompilerServices;
using GameLens.Shared.Models;
using GameLens.Shared.Services;

namespace GameLens.Endpoints;

internal static class CountersEndpoint
{
    internal static WebApplication MapCountersEndpoints(this WebApplication app)
    {
        app.MapGet("api/counters", Counters)
            .WithTags("Api");

        return app;
    }

    private static async IAsyncEnumerable<HeroCounter> Counters(ICounterService counterService, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (HeroCounter counter in counterService.GetCounters(cancellationToken))
        {
            yield return counter;
        }
    }
}

using System.Runtime.CompilerServices;
using GameLens.Shared.Models;
using GameLens.Shared.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GameLens.Endpoints;

internal static class HeroesEndpoint
{
    internal static WebApplication MapHeroesEndpoints(this WebApplication app)
    {
        app.MapGet("api/hero", Heroes)
            .WithTags("Hero");
        app.MapGet("api/hero/{name}/avatar", Avatar)
            .WithTags("Hero");

        return app;
    }

    private static async IAsyncEnumerable<Hero> Heroes(IHeroService heroService, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (Hero hero in heroService.GetHeroes(cancellationToken))
        {
            yield return hero;
        }
    }

    private static async Task<Results<IResult, NotFound>> Avatar(string name, IThumbnailService thumbnailService)
    {
        Stream thumbnailStream = await thumbnailService.GetHeroThumbnailAsync(name);

        return TypedResults.Stream(thumbnailStream, "image/png");
    }
}

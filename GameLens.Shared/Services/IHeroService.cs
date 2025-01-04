using GameLens.Shared.Models;

namespace GameLens.Shared.Services;

public interface IHeroService
{
    IAsyncEnumerable<Hero> GetHeroes(CancellationToken cancellationToken = default);
}
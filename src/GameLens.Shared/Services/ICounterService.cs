using GameLens.Shared.Models;

namespace GameLens.Shared.Services;

public interface ICounterService
{
    IAsyncEnumerable<HeroCounter> GetCounters(CancellationToken cancellationToken = default);
}

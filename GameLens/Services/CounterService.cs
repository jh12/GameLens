using System.Runtime.CompilerServices;
using System.Text.Json;
using GameLens.Models.Internal;
using GameLens.Shared.Models;
using GameLens.Shared.Services;

namespace GameLens.Services;

internal class CounterService : ICounterService
{
    private readonly IHeroService _heroService;
    private readonly string _rootPath;

    private readonly JsonSerializerOptions _serializerOptions;

    public CounterService(IHeroService heroService, IWebHostEnvironment hostEnvironment)
    {
        _heroService = heroService;

        _rootPath = hostEnvironment.WebRootPath;

        _serializerOptions = new JsonSerializerOptions()
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            PropertyNameCaseInsensitive = true
        };
    }

    public async IAsyncEnumerable<HeroCounter> GetCounters([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        Dictionary<string, Hero> heroMap = await _heroService.GetHeroes(cancellationToken).ToDictionaryAsync(h => h.Id, CancellationToken.None);

        string filePath = Path.Combine(_rootPath, "assets/data/Counters.json");

        await using FileStream stream = File.OpenRead(filePath);
        foreach (HeroCounterInternal counter in (await JsonSerializer.DeserializeAsync<HeroCounterInternal[]>(stream, _serializerOptions, cancellationToken))!)
        {
            Hero hero = heroMap[counter!.Id];
            Hero[] counterHeroes = counter.Counters?.Select(c => heroMap[c]).ToArray() ?? [];

            yield return new HeroCounter(hero, counterHeroes);
        }
    }
}

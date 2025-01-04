using System.Runtime.CompilerServices;
using System.Text.Json;
using GameLens.Shared.Models;
using GameLens.Shared.Services;

namespace GameLens.Services;

internal class HeroService : IHeroService
{
    private readonly string _rootPath;

    private readonly JsonSerializerOptions _serializerOptions;

    public HeroService(IWebHostEnvironment hostEnvironment)
    {
        _rootPath = hostEnvironment.WebRootPath;

        _serializerOptions = new JsonSerializerOptions()
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            PropertyNameCaseInsensitive = true
        };
    }

    public async IAsyncEnumerable<Hero> GetHeroes([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        string filePath = Path.Combine(_rootPath, "assets/data/Heroes.json");

        await using FileStream stream = File.OpenRead(filePath);
        await foreach (Hero hero in JsonSerializer.DeserializeAsyncEnumerable<Hero>(stream, _serializerOptions, cancellationToken))
        {
            yield return hero;
        }
    }
}

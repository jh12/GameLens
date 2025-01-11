using GameLens.Shared.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace GameLens.Services;

public class ThumbnailService : IThumbnailService
{
    private readonly string _rootPath;

    public ThumbnailService(IWebHostEnvironment hostEnvironment)
    {
        _rootPath = hostEnvironment.WebRootPath;
    }

    public Task<Stream> GetHeroThumbnailAsync(string id, CancellationToken cancellationToken = default)
    {
        string heroThumbnail = Path.Combine(_rootPath, "assets/heroes/thumbnails", id + ".png");

        return Task.FromResult<Stream>(File.OpenRead(heroThumbnail));
    }

    public async Task EnsureThumbnailsAsync()
    {
        string heroesFolder = Path.Combine(_rootPath, "assets/heroes");
        string thumbsFolder = Path.Combine(_rootPath, "assets/heroes/thumbnails");

        Directory.CreateDirectory(thumbsFolder);

        HashSet<string> existingHeroes = Directory
            .EnumerateFiles(thumbsFolder, "*.png", SearchOption.TopDirectoryOnly)
            .Select(Path.GetFileNameWithoutExtension)
            .ToHashSet()!;

        foreach (string file in Directory.EnumerateFiles(heroesFolder, "*.png", SearchOption.TopDirectoryOnly))
        {
            string heroName = Path.GetFileNameWithoutExtension(file);

            if (existingHeroes.Contains(heroName))
            {
                continue;
            }

            using var fullImage = await Image.LoadAsync(file);
            fullImage.Mutate(x => x.Resize(64, 64));

            await fullImage.SaveAsync(Path.Combine(thumbsFolder, heroName + ".png"));
        }
    }
}

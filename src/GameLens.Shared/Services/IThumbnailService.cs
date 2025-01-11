namespace GameLens.Shared.Services;

public interface IThumbnailService
{
    Task<Stream> GetHeroThumbnailAsync(string id, CancellationToken cancellationToken = default);

    Task EnsureThumbnailsAsync();
}

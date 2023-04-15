using DesktopUpdater.Interfaces;

namespace DesktopUpdater.Downloader;

public class BackgroundDownloader : IBackgroundDownloader
{
    private const string HttpWwwBingCom = "http://www.bing.com";

    private readonly ISizeProvider sizeProvider;
    private readonly ILogger logger;

    public BackgroundDownloader(ISizeProvider sizeProvider, ILogger logger)
    {
        this.sizeProvider = sizeProvider;
        this.logger = logger;
    }

    public async Task DownloadImageAsync(string urlBase, string backgroundJpgFile)
    {
        var size = sizeProvider.GetSize();
        var link = $"{HttpWwwBingCom}{urlBase}_{size.Width}x{size.Height}.jpg";

        using var client = new HttpClient();
        try
        {
            var response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync();
                using var fileStream = new FileStream(backgroundJpgFile, FileMode.Create);
                await stream.CopyToAsync(fileStream);
            }
            else
            {
                logger.Append($"Download image failed with status code: {response.StatusCode}.");
                throw new InvalidOperationException($"Download failed with status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            logger.Append($"Download image failed: {ex.Message}.");

            throw new InvalidOperationException(String.Concat($"Download failed from the following link: {link}",
                Environment.NewLine, ex.Message,
                Environment.NewLine, "Please edit the 'options.ini' file to download in different size."));
        }
    }
}

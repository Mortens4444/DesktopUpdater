namespace DesktopUpdater.Interfaces;

public interface IBackgroundDownloader
{
    Task DownloadImageAsync(string urlBase, string backgroundJpgFile);
}

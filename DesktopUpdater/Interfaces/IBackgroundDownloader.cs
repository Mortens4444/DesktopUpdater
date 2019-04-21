namespace DesktopUpdater.Interfaces
{
    public interface IBackgroundDownloader
    {
        bool DownloadImage(string urlBase, string backgroundJpgFile);
    }
}

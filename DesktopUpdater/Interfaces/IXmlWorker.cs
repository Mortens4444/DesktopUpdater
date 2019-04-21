namespace DesktopUpdater.Interfaces
{
    public interface IXmlWorker
    {
        string ImageName { get; }

        string DownloadImageAndGetName();
    }
}

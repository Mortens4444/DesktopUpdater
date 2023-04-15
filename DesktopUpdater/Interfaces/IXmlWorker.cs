namespace DesktopUpdater.Interfaces;

public interface IXmlWorker
{
    string? ImageName { get; }

    Task<string> DownloadImageAndGetNameAsync();
}

namespace DesktopUpdater.Interfaces;

public interface IXmlDownloader
{
    Task<string> GetXmlFileContentAsync();
}

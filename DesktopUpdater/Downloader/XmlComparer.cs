using DesktopUpdater.Interfaces;

namespace DesktopUpdater.Downloader;

public class XmlComparer : IXmlComparer
{
    public bool IsThisXmlExists(string xmlFile, string xmlFileContent)
    {
        if (File.Exists(xmlFile))
        {
            return FileUtils.GetFileContent(xmlFile) == xmlFileContent;
        }
        return false;
    }
}

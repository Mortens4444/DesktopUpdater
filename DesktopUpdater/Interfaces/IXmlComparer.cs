namespace DesktopUpdater.Interfaces
{
    public interface IXmlComparer
    {
        bool IsThisXmlExists(string xmlFile, string xmlFileContent);
    }
}

namespace DesktopUpdater.Interfaces;

public interface IOptionsFileCreator
{
    void CreateOptionsFileIfNotExists(string optionsFilename);
}

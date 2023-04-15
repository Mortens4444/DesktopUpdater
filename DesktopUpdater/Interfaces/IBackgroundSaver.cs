namespace DesktopUpdater.Interfaces;

public interface IBackgroundSaver
{
    void SaveImage(string path, string? imageName, string backgroundJpgFile);
}

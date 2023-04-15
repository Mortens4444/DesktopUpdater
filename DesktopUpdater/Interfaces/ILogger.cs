namespace DesktopUpdater.Interfaces;

public interface ILogger
{
    void Create(string logData);

    void Append(string logData);
}

using DesktopUpdater.Options;

namespace DesktopUpdater.Interfaces;

public interface IOptionsProvider
{
    OptionsDto Options { get; }
}

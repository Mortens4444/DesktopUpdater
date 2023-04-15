using DesktopUpdater.Interfaces;
using DesktopUpdater.Options;

namespace DesktopUpdater.Extras;

public class SizeProvider : ISizeProvider
{
    private readonly OptionsDto options;

    public SizeProvider(IOptionsProvider optionsProvider)
    {
        options = optionsProvider.Options;
    }

    public Size GetSize()
    {
        int width, height;

        if (!options.ForceCustomSize && Screen.PrimaryScreen != null)
        {
            width = Screen.PrimaryScreen.Bounds.Width;
            height = Screen.PrimaryScreen.Bounds.Height;
        }
        else
        {
            width = options.Width;
            height = options.Height;
        }
        return new Size(width, height);
    }
}

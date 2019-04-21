using DesktopUpdater.Interfaces;
using DesktopUpdater.Options;
using System.Drawing;
using System.Windows.Forms;

namespace DesktopUpdater.Extras
{
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

            if (options.ForceCustomSize)
            {
                width = options.Width;
                height = options.Height;
            }
            else
            {
                width = Screen.PrimaryScreen.Bounds.Width;
                height = Screen.PrimaryScreen.Bounds.Height;
            }
            return new Size(width, height);
        }
    }
}

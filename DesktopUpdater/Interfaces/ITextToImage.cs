using System.Drawing;

namespace DesktopUpdater.Interfaces
{
    public interface ITextToImage
    {
        Bitmap AddText(Image image, Font font, string text, Rectangle region);
    }
}

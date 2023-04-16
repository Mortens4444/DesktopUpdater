using DesktopUpdater.Interfaces;
using MessageBoxes;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace DesktopUpdater.Extras;

public class TextToImage : ITextToImage
{
    private const sbyte Shadow = 1;
    private const sbyte Shadow2 = 2;

    public Bitmap? AddText(Image image, Font font, string text, Rectangle region)
    {
        if (image == null)
        {
            return null;
        }

        var bmp = new Bitmap(image);
        try
        {
            var result = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb);
            WriteTextToBitmapWithShadow(result, region, text, font);
            return result;
        }
        catch (Exception ex)
        {
            ErrorBox.Show(ex, Timeout.Infinite);
        }
        return bmp;
    }

    public static Size MeasureText(Size proposedSize, Font font, string text)
    {
        return TextRenderer.MeasureText(text, font, proposedSize, GetTextFormatFlags());
    }

    private static TextFormatFlags GetTextFormatFlags()
    {
        return TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix;
    }

    private static void WriteTextToBitmapWithShadow(Image image, Rectangle region, string text, Font font)
    {
        var flags = GetTextFormatFlags();
        var x = region.X;
        var y = region.Y;

        using var graphics = Graphics.FromImage(image);
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

        var directions = new List<(sbyte horizontal, sbyte vertical)>()
            {
                (horizontal: 0, vertical: -Shadow2), // North
                (horizontal: -Shadow2, vertical: 0), // West
                (horizontal: Shadow2, vertical: -Shadow2), // North-east
                (horizontal: 0, vertical: Shadow2), // South
                (horizontal: -Shadow2, vertical: Shadow2), // South-west
                (horizontal: -Shadow2, vertical: -Shadow2), // North-west
                (horizontal: Shadow2, vertical: 0), // East
                (horizontal: Shadow2, vertical: Shadow2), // South-east

                (horizontal: 0, vertical: -Shadow), // North
                (horizontal: -Shadow, vertical: 0), // West
                (horizontal: Shadow, vertical: -Shadow), // North-east
                (horizontal: 0, vertical: Shadow), // South
                (horizontal: -Shadow, vertical: Shadow), // South-west
                (horizontal: -Shadow, vertical: -Shadow), // North-west
                (horizontal: Shadow, vertical: 0), // East
                (horizontal: Shadow, vertical: Shadow), // South-east
            };
        foreach (var (horizontal, vertical) in directions)
        {
            region.Location = new Point(x + horizontal, y + vertical);
            TextRenderer.DrawText(graphics, text, font, region, Color.Black, flags);
        }

        region.Location = new Point(x, y);
        TextRenderer.DrawText(graphics, text, font, region, Color.White, flags);
    }
}

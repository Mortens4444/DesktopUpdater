using DesktopUpdater.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;

namespace DesktopUpdater.Extras;

public class ImageTextCreator : IImageTextCreator
{
    private readonly Font font;

    public Image? Image { get; private set; }

    private readonly ITextToImage textToImage;

    public ImageTextCreator(ITextToImage textToImage)
    {
        this.textToImage = textToImage;
        var fontFamily = new FontFamily("Tahoma");
        font = new Font(fontFamily, 16, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void Initialize(string inputFilename)
    {
        Image = File.Exists(inputFilename) ? Image.FromFile(inputFilename) : null;
    }

    public void AddTextToImage(string textToShow, Rectangle region)
    {
        if (Image == null)
        {
            return;
        }

        Image = textToImage.AddText(Image, font, textToShow, region);
    }

    public void AddTextToTopLeft(string textToShow, Rectangle region, int? leftMargin = null, int? topMargin = null)
    {
        var size = GetSize(textToShow, region.Size);
        var xCoordinate = leftMargin ?? region.Left;
        var yCoordinate = topMargin ?? region.Top;
        AddTextToImage(textToShow, GetRectangle(xCoordinate, yCoordinate, size));
    }

    public void AddTextToTopRight(string textToShow, Rectangle region, int? rightMargin = null, int? topMargin = null)
    {
        if (Image == null)
        {
            return;
        }

        var size = GetSize(textToShow, region.Size);
        var width = Image.Width - size.Width;
        var xCoordinate = rightMargin.HasValue ? width - rightMargin.Value : width - region.Left;
        var yCoordinate = topMargin ?? region.Top;
        AddTextToImage(textToShow, GetRectangle(xCoordinate, yCoordinate, size));
    }

    public void AddTextToBottomLeft(string textToShow, Rectangle region, int? leftMargin = null, int? bottomMargin = null)
    {
        if (Image == null)
        {
            return;
        }

        var size = GetSize(textToShow, region.Size);
        var xCoordinate = leftMargin ?? region.Left;
        var height = Image.Height - size.Height;
        var yCoordinate = bottomMargin.HasValue ? height - bottomMargin.Value : height - region.Top;
        AddTextToImage(textToShow, GetRectangle(xCoordinate, yCoordinate, size));
    }

    public void AddTextToBottomRight(string textToShow, Rectangle region, int? rightMargin = null, int? bottomMargin = null)
    {
        if (Image == null)
        {
            return;
        }

        var size = GetSize(textToShow, region.Size);
        var width = Image.Width - size.Width;
        var xCoordinate = rightMargin.HasValue ? width - rightMargin.Value : width - region.Left;
        var height = Image.Height - size.Height;
        var yCoordinate = bottomMargin.HasValue ? height - bottomMargin.Value : height - region.Top;
        AddTextToImage(textToShow, GetRectangle(xCoordinate, yCoordinate, size));
    }

    public void AddTextToTopCenter(string textToShow, Rectangle region, int? topMargin = null)
    {
        if (Image == null)
        {
            return;
        }

        var size = GetSize(textToShow, region.Size);
        var xCoordinate = (Image.Width - size.Width) / 2;
        var yCoordinate = topMargin ?? region.Top;
        AddTextToImage(textToShow, GetRectangle(xCoordinate, yCoordinate, size));
    }

    public void AddTextToBottomCenter(string textToShow, Rectangle region, int? bottomMargin = null)
    {
        if (Image == null)
        {
            return;
        }

        var size = GetSize(textToShow, region.Size);
        var xCoordinate = (Image.Width - size.Width) / 2;
        var height = Image.Height - size.Height;
        var yCoordinate = bottomMargin.HasValue ? height - bottomMargin.Value : height - region.Top;
        AddTextToImage(textToShow, GetRectangle(xCoordinate, yCoordinate, size));
    }

    public void AddTextToMiddleCenter(string textToShow, Rectangle region)
    {
        if (Image == null)
        {
            return;
        }

        var size = GetSize(textToShow, region.Size);
        var xCoordinate = (Image.Width - size.Width) / 2;
        var yCoordinate = (Image.Height - size.Height) / 2;
        AddTextToImage(textToShow, GetRectangle(xCoordinate, yCoordinate, size));
    }

    private Size GetSize(string textToShow, Size proposedSize)
    {
        return TextToImage.MeasureText(proposedSize, font, textToShow);
    }

    private static Rectangle GetRectangle(int xCoordinate, int yCoordinate, Size size)
    {
        var location = new Point(xCoordinate, yCoordinate);
        return new Rectangle(location, size);
    }

    public bool SaveAsBitmap(string outputFilename)
    {
        if (Image == null)
        {
            return false;
        }

        var bitmap = new Bitmap(Image);
        bitmap.Save(outputFilename, ImageFormat.Bmp);
        return true;
    }
}

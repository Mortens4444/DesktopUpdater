using System.Drawing;

namespace DesktopUpdater.Interfaces;

public interface IImageTextCreator
{
    Image? Image { get; }

    void Initialize(string inputFilename);

    void AddTextToBottomCenter(string textToShow, Rectangle region, int? bottomMargin = null);
    void AddTextToBottomLeft(string textToShow, Rectangle region, int? leftMargin = null, int? bottomMargin = null);
    void AddTextToBottomRight(string textToShow, Rectangle region, int? rightMargin = null, int? bottomMargin = null);
    void AddTextToImage(string textToShow, Rectangle region);
    void AddTextToMiddleCenter(string textToShow, Rectangle region);
    void AddTextToTopCenter(string textToShow, Rectangle region, int? topMargin = null);
    void AddTextToTopLeft(string textToShow, Rectangle region, int? leftMargin = null, int? topMargin = null);
    void AddTextToTopRight(string textToShow, Rectangle region, int? rightMargin = null, int? topMargin = null);
    bool SaveAsBitmap(string outputFilename);
}
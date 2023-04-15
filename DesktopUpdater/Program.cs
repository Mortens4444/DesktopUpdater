using DesktopUpdater.Interfaces;
using DesktopUpdater.MessageBoxes;

namespace DesktopUpdater;

static class Program
{
    private const string BACKGROUND_BMP = "background.bmp";

    static void Main(string[] args)
    {
        var dependencyInjection = new DependencyInjection();

        var logger = dependencyInjection.Get<ILogger>();
        logger.Create("DesktopUpdater has been started.");

        var optionsProvider = dependencyInjection.Get<IOptionsProvider>();
        var options = optionsProvider.Options;

        var xmlWorker = dependencyInjection.Get<IXmlWorker>();
        var backgroundChanger = dependencyInjection.Get<IBackgroundChanger>();
        var backgroundSaver = dependencyInjection.Get<IBackgroundSaver>();
        var imageTextCreator = dependencyInjection.Get<IImageTextCreator>();

        var numberOfTries = 0;
        while (numberOfTries < options.NumberOfAttemptsToDownloadBackground)
        {
            try
            {
                numberOfTries++;
                logger.Append($"Trying to download image. Attempts: {numberOfTries}.");
                var backgroundJpgFileTask = xmlWorker.DownloadImageAndGetNameAsync();
                var backgroundJpgFile = backgroundJpgFileTask.Result;
                logger.Append($"Background JPG filename: {backgroundJpgFile}.");
                if (!String.IsNullOrEmpty(backgroundJpgFile))
                {
                    var saveImage = (args.Length == 2) && (IsSave(args[0].ToLower()));
                    if (saveImage)
                    {
                        backgroundSaver.SaveImage(args[1], xmlWorker.ImageName, backgroundJpgFile);
                    }
                    else
                    {
                        var outputFilename = Path.Combine(AppContext.BaseDirectory, BACKGROUND_BMP);

                        imageTextCreator.Initialize(backgroundJpgFile);
                        if (imageTextCreator.Image == null)
                        {
                            throw new ArgumentNullException(nameof(imageTextCreator), $"Cannot be null: {nameof(imageTextCreator)}.{nameof(imageTextCreator.Image)}");
                        }

                        const int locationX = 200;
                        const int width = 1000;
                        const int height = 80;
                        var location = new Point(locationX, 30);

                        const int nameHeight = 30;
                        if (options.ShowImageInfo && xmlWorker.ImageName != null)
                        {
                            imageTextCreator.AddTextToTopCenter(xmlWorker.ImageName, new Rectangle(location, new Size(width, nameHeight)));
                        }

                        var now = DateTime.Now;
                        if (options.ShowNamedays)
                        {
                            var hungarianNameDayProvider = dependencyInjection.Get<IHungarianNameDayProvider>();
                            imageTextCreator.AddTextToTopLeft(hungarianNameDayProvider.GetNameDayText(now), new Rectangle(locationX, location.Y + nameHeight, width, height));
                        }
                        if (options.ShowBirthdays)
                        {
                            var birthDayProvider = dependencyInjection.Get<IBirthDayProvider>();
                            imageTextCreator.AddTextToTopRight(birthDayProvider.GetBirthDayText(now), new Rectangle(locationX, location.Y + nameHeight, width, height));
                        }

                        const int margin = 20;
                        const int quationHeight = 90;
                        var quotationProvider = dependencyInjection.Get<IQuotationProvider>();
                        if (options.ShowQuotation)
                        {
                            imageTextCreator.AddTextToTopCenter(quotationProvider.GetQuotation(), new Rectangle(locationX, location.Y + height + nameHeight, imageTextCreator.Image.Width - 2 * locationX, imageTextCreator.Image.Height - 2 * quationHeight));
                        }

                        try
                        {
                            if (options.FixQuotationNumber1 != 0)
                            {
                                imageTextCreator.AddTextToBottomLeft(quotationProvider.GetQuotation(options.FixQuotationNumber1), new Rectangle(margin, 2 * quationHeight, imageTextCreator.Image.Width - margin, imageTextCreator.Image.Height - quationHeight));
                            }
                            if (options.FixQuotationNumber2 != 0)
                            {
                                imageTextCreator.AddTextToBottomLeft(quotationProvider.GetQuotation(options.FixQuotationNumber2), new Rectangle(margin, quationHeight, imageTextCreator.Image.Width - margin, imageTextCreator.Image.Height));
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Append($"Some error occurred: {ex.Message}.");                        
                        }

                        imageTextCreator.SaveAsBitmap(outputFilename);
                        backgroundChanger.ChangeBackground(outputFilename);
                    }
                    break;
                }
            }
            catch (Exception ex)
            {
                logger.Append($"Some error occurred: {ex.Message}.");
                ErrorBox.Show(ex);
            }
            finally
            {
                Thread.Sleep(options.WaitBetweenDownloadAttemptsInMiliseconds);
            }
        }
    }

    private static bool IsSave(string arg)
    {
        if ((arg == "-s") || (arg == "/s") || (arg == "\\s") || (arg == "--save") || (arg == "/save") || (arg == "\\save"))
        {
            return true;
        }

        return false;
    }
}

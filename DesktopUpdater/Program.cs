using DesktopUpdater.Interfaces;
using MessageBoxes;
using Ninject;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace DesktopUpdater
{
    static class Program
    {
        private const string BACKGROUND_BMP = "background.bmp";

        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load<DefaultNinjectModule>();

            var resourceFileCreator = kernel.Get<IResourceFileCreator>();
            resourceFileCreator.Create("clear.bmp");

            var logger = kernel.Get<ILogger>();
            logger.Create("DesktopUpdater has been started.");

            var optionsProvider = kernel.Get<IOptionsProvider>();
            var options = optionsProvider.Options;

            var xmlWorker = kernel.Get<IXmlWorker>();
            var backgroundChanger = kernel.Get<IBackgroundChanger>();
            var backgroundSaver = kernel.Get<IBackgroundSaver>();
            var imageTextCreator = kernel.Get<IImageTextCreator>();

            var numberOfTries = 0;
            while (numberOfTries < options.NumberOfAttemptsToDownloadBackground)
            {
                try
                {
                    numberOfTries++;
                    logger.Append($"Trying to download image. Attempts: {numberOfTries}.");
                    var backgroundJpgFile = xmlWorker.DownloadImageAndGetName();
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
                            var outputFilename = Path.Combine(Application.StartupPath, BACKGROUND_BMP);

                            imageTextCreator.Initialize(backgroundJpgFile);

                            const int locationX = 200;
                            const int width = 1000;
                            const int height = 80;
#if __MonoCS__
                            var location = new Point(locationX, 100);
#else
                            var location = new Point(locationX, 30);
#endif

                            const int nameHeight = 30;
                            if (options.ShowImageInfo)
                            {
                                imageTextCreator.AddTextToTopCenter(xmlWorker.ImageName, new Rectangle(location, new Size(width, nameHeight)));
                            }

                            var now = DateTime.Now;
                            if (options.ShowNamedays)
                            {
                                var hungarianNameDayProvider = kernel.Get<IHungarianNameDayProvider>();
                                imageTextCreator.AddTextToTopLeft(hungarianNameDayProvider.GetNameDayText(now), new Rectangle(locationX, location.Y + nameHeight, width, height));
                            }
                            if (options.ShowBirthdays)
                            {
                                var birthDayProvider = kernel.Get<IBirthDayProvider>();
                                imageTextCreator.AddTextToTopRight(birthDayProvider.GetBirthDayText(now), new Rectangle(locationX, location.Y + nameHeight, width, height));
                            }

                            const int margin = 20;
                            const int quationHeight = 90;
                            var quotationProvider = kernel.Get<IQuotationProvider>();
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
                            catch { }

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
}

using DesktopUpdater.Interfaces;
using System;
using System.Net;

namespace DesktopUpdater.Downloader
{
    public class BackgroundDownloader : IBackgroundDownloader
    {
        private const string HttpWwwBingCom = "http://www.bing.com";

        private readonly ISizeProvider sizeProvider;
        private readonly ILogger logger;

        public BackgroundDownloader(ISizeProvider sizeProvider,
            ILogger logger)
        {
            this.sizeProvider = sizeProvider;
            this.logger = logger;
        }

        public bool DownloadImage(string urlBase, string backgroundJpgFile)
        {
            var size = sizeProvider.GetSize();
            var link = $"{HttpWwwBingCom}{urlBase}_{size.Width}x{size.Height}.jpg";
            var client = new WebClient();

            try
            {
                client.DownloadFile(link, backgroundJpgFile);
                return true;
            }
            catch (Exception ex)
            {
                logger.Append($"Download image failed: {ex.Message}.");

                throw new InvalidOperationException(String.Concat($"Download failed from the following link: {link}",
                    Environment.NewLine, ex.Message,
                    Environment.NewLine, "Please edit the 'options.ini' file to download in different size."));
            }
        }
    }
}

﻿using DesktopUpdater.Interfaces;

namespace DesktopUpdater.Downloader;

public class XmlWorker : IXmlWorker
{
    private static readonly string xmlFile = Path.Combine(AppContext.BaseDirectory, "background.xml");
    private static readonly string backgroundJpgFile = Path.Combine(AppContext.BaseDirectory, "background.jpg");

    private readonly ILogger logger;
    private readonly IXmlDownloader xmlDownloader;
    private readonly IXmlComparer xmlComparer;
    private readonly IBackgroundDownloader backgroundDownloader;
    private readonly IOptionsProvider optionsProvider;

    public string? ImageName { get; private set; }

    public XmlWorker(ILogger logger,
        IXmlDownloader xmlDownloader,
        IXmlComparer xmlComparer,
        IBackgroundDownloader backgroundDownloader,
        IOptionsProvider optionsProvider)
    {
        this.logger = logger;
        this.xmlDownloader = xmlDownloader;
        this.xmlComparer = xmlComparer;
        this.backgroundDownloader = backgroundDownloader;
        this.optionsProvider = optionsProvider;
    }

    public async Task<string> DownloadImageAndGetNameAsync()
    {
        var xmlFileContent = xmlDownloader.GetXmlFileContentAsync().GetAwaiter().GetResult();
        if (String.IsNullOrEmpty(xmlFileContent))
        {
            return String.Empty;
        }

        var isThisXmlExists = xmlComparer.IsThisXmlExists(xmlFile, xmlFileContent);
        if (optionsProvider.Options.ForceDownloadImage || !isThisXmlExists || (!File.Exists(backgroundJpgFile)))
        {
            var urlBase = xmlFileContent.Substring("<urlBase>", "</urlBase>");
            logger.Append($"Should use URL: {urlBase}.");
            await backgroundDownloader.DownloadImageAsync(urlBase, backgroundJpgFile);
            FileUtils.WriteToTextFile(xmlFile, xmlFileContent, true, false);
        }
        else
        {
            var message = isThisXmlExists ?
                "Won't download image because the same file has been already downloaded." :
                "Won't download image.";
            logger.Append(message);
        }

        ImageName = HtmlCharacterEntityReplacer.Replace(xmlFileContent.Substring("<copyright>", "</copyright>"));
        return backgroundJpgFile;
    }
}

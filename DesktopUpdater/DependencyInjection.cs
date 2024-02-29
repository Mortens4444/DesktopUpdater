using DesktopUpdater.Background;
using DesktopUpdater.Downloader;
using DesktopUpdater.Extras;
using DesktopUpdater.Interfaces;
using DesktopUpdater.Options;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopUpdater;

public class DependencyInjection
{
    private readonly ServiceProvider serviceProvider;

    public DependencyInjection()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IOptionsFileCreator, OptionsFileCreator>();
        serviceCollection.AddSingleton<IOptionsProvider, OptionsProvider>();

        serviceCollection.AddScoped<IBackgroundChanger, BackgroundChanger>();
        serviceCollection.AddScoped<IBackgroundSaver, BackgroundSaver>();
        serviceCollection.AddScoped<IBackgroundDownloader, BackgroundDownloader>();

        serviceCollection.AddScoped<IXmlDownloader, XmlDownloader>();
        serviceCollection.AddScoped<IXmlWorker, XmlWorker>();
        serviceCollection.AddScoped<IXmlComparer, XmlComparer>();

        serviceCollection.AddScoped<IQuotationProvider, QuotationProvider>();
        serviceCollection.AddScoped<IHungarianNameDayProvider, HungarianNameDayProvider>();
        serviceCollection.AddScoped<IBirthDayProvider, BirthDayProvider>();
        serviceCollection.AddScoped<ITextToImage, TextToImage>();
        serviceCollection.AddScoped<IImageTextCreator, ImageTextCreator>();
        serviceCollection.AddScoped<ISizeProvider, SizeProvider>();
        serviceCollection.AddScoped<ILogger, Logger>();
        serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public T Get<T>()
        where T : notnull
    {
        return serviceProvider.GetRequiredService<T>();
    }
}

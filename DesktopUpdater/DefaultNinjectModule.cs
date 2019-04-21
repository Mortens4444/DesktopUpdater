using DesktopUpdater.Background;
using DesktopUpdater.Downloader;
using DesktopUpdater.Extras;
using DesktopUpdater.Interfaces;
using DesktopUpdater.Options;
using Ninject.Modules;
using System;

namespace DesktopUpdater
{
    public class DefaultNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IOptionsFileCreator>().To<OptionsFileCreator>();
            Kernel.Bind<IOptionsProvider>().To<OptionsProvider>().InSingletonScope();

            Kernel.Bind<IBackgroundChanger>().To<BackgroundChanger>();
            Kernel.Bind<IBackgroundSaver>().To<BackgroundSaver>();
            Kernel.Bind<IBackgroundDownloader>().To<BackgroundDownloader>();

            Kernel.Bind<IXmlDownloader>().To<XmlDownloader>();
            Kernel.Bind<IXmlWorker>().To<XmlWorker>();
            Kernel.Bind<IXmlComparer>().To<XmlComparer>();

            Kernel.Bind<IQuotationProvider>().To<QuotationProvider>();
            Kernel.Bind<IHungarianNameDayProvider>().To<HungarianNameDayProvider>();
            Kernel.Bind<IBirthDayProvider>().To<BirthDayProvider>();
            Kernel.Bind<IDateToStringFormat>().To<DateToStringFormat>();
            Kernel.Bind<IResourceExtractor>().To<ResourceExtractor>();
            Kernel.Bind<IResourceFileCreator>().To<ResourceFileCreator>();
            Kernel.Bind<ITextToImage>().To<TextToImage>();
            Kernel.Bind<IImageTextCreator>().To<ImageTextCreator>();
            Kernel.Bind<ISizeProvider>().To<SizeProvider>();

            Kernel.Bind<ILogger>().To<Logger>();
        }
    }
}

using DesktopUpdater.Interfaces;
using System;
using System.IO;
using System.Windows.Forms;

namespace DesktopUpdater.Extras
{
    public class ResourceFileCreator : IResourceFileCreator
    {
        private readonly IResourceExtractor resourceExtractor;

        public ResourceFileCreator(IResourceExtractor resourceExtractor)
        {
            this.resourceExtractor = resourceExtractor;
        }

        public void Create(string fileName)
        {
            var fileLocation = Path.Combine(Application.StartupPath, fileName);
            if (!File.Exists(fileLocation))
            {
                resourceExtractor.Extract(String.Concat(Application.ProductName, ".Resources.", fileName), fileLocation);
            }
        }
    }
}

using DesktopUpdater.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUpdater.Downloader
{
    public class XmlComparer : IXmlComparer
    {
        public bool IsThisXmlExists(string xmlFile, string xmlFileContent)
        {
            if (File.Exists(xmlFile))
            {
                return FileUtils.GetFileContent(xmlFile) == xmlFileContent;
            }
            return false;
        }
    }
}

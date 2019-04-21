using DesktopUpdater.Interfaces;
using System.IO;
using System.Reflection;

namespace DesktopUpdater.Extras
{
    public class ResourceExtractor : IResourceExtractor
    {
        public void Extract(string resoureName, string extractToLocation)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceFileStream = assembly.GetManifestResourceStream(resoureName);
            if (resourceFileStream != null)
            {
                using (var binaryReader = new BinaryReader(resourceFileStream))
                {
                    using (var fileStream = new FileStream(extractToLocation, FileMode.CreateNew))
                    {
                        using (var binaryWriter = new BinaryWriter(fileStream))
                        {
                            var byteArray = new byte[resourceFileStream.Length];
                            resourceFileStream.Read(byteArray, 0, byteArray.Length);
                            binaryWriter.Write(byteArray);
                            binaryWriter.Close();
                        }
                        fileStream.Close();
                        resourceFileStream.Close();
                    }
                    binaryReader.Close();
                }
            }
        }
    }
}

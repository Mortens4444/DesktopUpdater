using DesktopUpdater.Interfaces;
using System.IO;

namespace DesktopUpdater.Options
{
    public class OptionsFileCreator : IOptionsFileCreator
    {
        public void CreateOptionsFileIfNotExists(string optionsFilename)
        {
            if (File.Exists(optionsFilename))
            {
                return;
            }

            var options = new OptionsDto();
            using (var sw = File.CreateText(optionsFilename))
            {
                var properties = options.GetType().GetProperties();
                foreach (var property in properties)
                {
                    sw.WriteLine($"{property.Name} = {property.GetValue(options)}");
                }
                sw.Close();
            }
        }
    }
}

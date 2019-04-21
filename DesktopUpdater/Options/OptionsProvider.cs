using DesktopUpdater.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DesktopUpdater.Options
{
    public class OptionsProvider : IOptionsProvider
    {
        public OptionsDto Options { get; private set; }

        private readonly string optionsFilename = Path.Combine(Application.StartupPath, "options.ini");

        public OptionsProvider(IOptionsFileCreator optionsFileCreator)
        {
            optionsFileCreator.CreateOptionsFileIfNotExists(optionsFilename);
            Options = GetOptions();
        }

        private OptionsDto GetOptions()
        {
            var result = new OptionsDto();
            var optionsFileContent = File.ReadAllText(optionsFilename);
            var options = optionsFileContent.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var option in options)
            {
                try
                {
                    var nameAndValue = option.Split('=');
                    var name = nameAndValue.First().Trim();
                    var value = nameAndValue.Last().Trim();
                    var property = typeof(OptionsDto).GetProperty(name);
                    var convertedValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(result, convertedValue);
                }
                catch { }
            }
            return result;
        }
    }
}

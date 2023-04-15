using DesktopUpdater.Interfaces;

namespace DesktopUpdater.Options;

public class OptionsProvider : IOptionsProvider
{
    public OptionsDto Options { get; private set; }

    private readonly string optionsFilename = Path.Combine(AppContext.BaseDirectory, "options.ini");

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
                if (property != null)
                {
                    var convertedValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(result, convertedValue);
                }
            }
            catch { }
        }
        return result;
    }
}

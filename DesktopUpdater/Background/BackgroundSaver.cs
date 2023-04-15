using DesktopUpdater.Interfaces;
using DesktopUpdater.MessageBoxes;
using System.Text.RegularExpressions;

namespace DesktopUpdater.Background;

public class BackgroundSaver : IBackgroundSaver
{
    public void SaveImage(string path, string? imageName, string backgroundJpgFile)
    {
        if (imageName == null)
        {
            throw new ArgumentNullException(nameof(imageName));
        }

        var rgx = new Regex(@"[/\\:*?<>|]");
        imageName = rgx.Replace(imageName, "-");
        var filename = $"{Path.Combine(path, imageName)}.jpg";

        var dialogResult = DialogResult.Yes;
        if (File.Exists(filename))
        {
            dialogResult = ConfirmBox.Show("File already exists", String.Concat("Do you want to overwrite existing file?", Environment.NewLine, filename));
        }

        if (dialogResult != DialogResult.Yes)
        {
            return;
        }

        filename = Environment.ExpandEnvironmentVariables(filename);
        File.Copy(backgroundJpgFile, filename, true);
        InfoBox.Show("Desktop image saved", $"Successfully saved as: {filename}");
    }
}

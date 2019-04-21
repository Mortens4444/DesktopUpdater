using DesktopUpdater.Interfaces;
using Enums;
using MessageBoxes;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DesktopUpdater.Background
{
    public class BackgroundSaver : IBackgroundSaver
    {
        public void SaveImage(string path, string imageName, string backgroundJpgFile)
        {
            var rgx = new Regex(@"[/\\:*?<>|]");
            imageName = rgx.Replace(imageName, "-");
            var filename = $"{Path.Combine(path, imageName)}.jpg";

            var dialogResult = DialogResult.Yes;
            if (File.Exists(filename))
            {
                dialogResult = ConfirmBox.Show("File already exists", String.Concat("Do you want to overwrite existing file?", Environment.NewLine, filename), Decide.No);
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
}

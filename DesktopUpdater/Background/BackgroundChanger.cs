using DesktopUpdater.Interfaces;
using Enums;
using MessageBoxes;
using System;
#if __MonoCS__
using System.Diagnostics;
#endif
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DesktopUpdater.Background
{
    public class BackgroundChanger : IBackgroundChanger
    {
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(SystemParameterInfoActionType uiAction, uint uiParam, String pvParam, SPIF fWinIni);

        private const string ClearBmp = "clear.bmp";

        public void ChangeBackground(string filename)
        {
            var backgoundChanged = SetBackgroundBmp(Path.Combine(Application.StartupPath, ClearBmp)); // Must clear on Windows 10
            if (!backgoundChanged)
            {
                return;
            }
            SetBackgroundBmp(filename);
        }

        private static bool SetBackgroundBmp(string filename)
        {
#if !__MonoCS__
                var spif = SPIF.SPIF_SENDCHANGE | SPIF.SPIF_UPDATEINIFILE | SPIF.SPIF_SENDWININICHANGE; // SPIF.SPIF_UPDATEINIFILE | SPIF.SPIF_SENDWININICHANGE
                if (!SystemParametersInfo(SystemParameterInfoActionType.SPI_SETDESKWALLPAPER, 0, filename, spif))
                {
                    ErrorBox.ShowLastWin32Error();
                    return false;
                }
#else
                var process = new Process();
                process.StartInfo.FileName = "dconf";
                process.StartInfo.Arguments = String.Concat("write \"/org/gnome/desktop/background/picture-uri\" \"'file://", filename, "'\"");
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.WaitForExit();
                var error = process.StandardError.ReadToEnd();
                if (!String.IsNullOrWhiteSpace(error))
                {
                    ErrorBox.Show("Change background error", error);
                    return false;
                }
#endif

            return true;
        }
    }
}
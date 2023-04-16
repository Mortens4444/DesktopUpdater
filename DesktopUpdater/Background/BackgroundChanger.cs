using DesktopUpdater.Interfaces;
using MessageBoxes;
using System.Runtime.InteropServices;

namespace DesktopUpdater.Background;

public class BackgroundChanger : IBackgroundChanger
{
    [DllImport("User32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

    private const uint SPI_SETDESKWALLPAPER = 0x0014;
    private const uint SPIF_UPDATEINIFILE = 0x01;
    private const uint SPIF_SENDCHANGE = 0x02;

    public void ChangeBackground(string filename)
    {
        SetBackgroundBmp(filename);
    }

    private static bool SetBackgroundBmp(string filename)
    {
        if (!SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, filename, SPIF_SENDCHANGE | SPIF_UPDATEINIFILE))
        {
            ErrorBox.ShowLastWin32Error();
            return false;
        }

        return true;
    }
}
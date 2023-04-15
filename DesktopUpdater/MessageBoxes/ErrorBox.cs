using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DesktopUpdater.MessageBoxes;

public static class ErrorBox
{
    public static void Show(Exception exception)
    {
        MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
    }

    public static void ShowLastWin32Error()
    {
        var errorCode = Marshal.GetLastWin32Error();
        var exception = new Win32Exception(errorCode);
        Show(exception);
    }
}

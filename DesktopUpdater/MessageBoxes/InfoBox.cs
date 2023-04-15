namespace DesktopUpdater.MessageBoxes;

public static class InfoBox
{
    public static void Show(string caption, string message)
    {
        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
    }
}

namespace DesktopUpdater.MessageBoxes;

public static class ConfirmBox
{
    public static DialogResult Show(string caption, string message)
    {
        return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
    }
}

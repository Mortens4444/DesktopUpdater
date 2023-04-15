namespace DesktopUpdater;

public static class FileUtils
{
    public static string GetFileContent(string filename)
    {
        if (!File.Exists(filename))
        {
            throw new FileNotFoundException($"File not found: {filename}", filename);
        }

        string result;
        using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
        {
            using (var streamReader = new StreamReader(fileStream))
            {
                result = streamReader.ReadToEnd();
                streamReader.Close();
            }
            fileStream.Close();
        }
        return result;
    }

    public static Exception? WriteToTextFile(string filename, string data, bool overwrite, bool useWriteline)
    {
        Exception? result = null;
        try
        {
            using var streamWriter = overwrite ? File.CreateText(filename) : File.AppendText(filename);
            WriteToStream(data, useWriteline, streamWriter);
        }
        catch (Exception ex)
        {
            result = ex;
        }
        return result;
    }

    private static void WriteToStream(string data, bool useWriteline, StreamWriter streamWriter)
    {
        if (useWriteline)
        {
            streamWriter.WriteLine(data);
        }
        else
        {
            streamWriter.Write(data);
        }
        streamWriter.Close();
    }
}

using Consts;
using System;
using System.IO;

namespace DesktopUpdater
{
    public static class FileUtils
    {
        public static string GetFileContent(string filename)
        {
            string result;
            if (File.Exists(filename))
            {
                using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        result = streamReader.ReadToEnd();
                        streamReader.Close();
                    }
                    fileStream.Close();
                }
            }
            else throw new FileNotFoundException(String.Concat(Constants.FILE_NOT_FOUND, filename), filename);
            return result;
        }

        public static Exception WriteToTextFile(string filename, string data, bool overwrite, bool useWriteline)
        {
            Exception result = null;
            try
            {
                if (overwrite)
                {
                    using (var sw = File.CreateText(filename))
                    {
                        if (useWriteline) sw.WriteLine(data);
                        else sw.Write(data);
                        sw.Close();
                    }
                }
                else
                {
                    using (var sw = File.AppendText(filename))
                    {
                        if (useWriteline) sw.WriteLine(data);
                        else sw.Write(data);
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex;
            }
            return result;
        }
    }
}

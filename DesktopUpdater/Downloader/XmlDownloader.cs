using DesktopUpdater.Interfaces;

namespace DesktopUpdater.Downloader;

public class XmlDownloader : IXmlDownloader
{
    public async Task<string> GetXmlFileContentAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "http://www.bing.com/hpimagearchive.aspx?format=xml&idx=0&n=1&mbl=1&mkt=en-ww");
        var httpClient = new HttpClient
        {
            Timeout = Timeout.InfiniteTimeSpan
        };
        using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        if (!response.IsSuccessStatusCode)
        {
            return string.Empty;
        }

         return await response.Content.ReadAsStringAsync();
    }
}

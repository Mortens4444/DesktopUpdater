﻿using DesktopUpdater.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Threading;

namespace DesktopUpdater.Downloader
{
    public class XmlDownloader : IXmlDownloader
    {
        public string GetXmlFileContent()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.bing.com/hpimagearchive.aspx?format=xml&idx=0&n=1&mbl=1&mkt=en-ww");
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            request.Timeout = Timeout.Infinite;

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response == null)
                {
                    return String.Empty;
                }

                var responseStream = response.GetResponseStream();
                if (responseStream == null)
                {
                    return String.Empty;
                }

                responseStream.ReadTimeout = Timeout.Infinite;
                using (var responseStreamReader = new StreamReader(responseStream))
                {
                    responseStreamReader.BaseStream.ReadTimeout = Timeout.Infinite;
                    return responseStreamReader.ReadToEnd();
                }
            }
        }
    }
}

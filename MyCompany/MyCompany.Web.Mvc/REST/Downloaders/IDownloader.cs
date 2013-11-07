using System;
using System.Collections.Generic;

namespace MyCompany.Web.Mvc.REST.Downloaders
{
    public interface IDownloader
    {
        int RequestTimeout { get; set; }
        DownloaderResponse GetResponse(Uri uri);
        DownloaderResponse GetResponse(Uri uri, HttpRequestMethod method, Dictionary<string, string> parameters);
    }
}

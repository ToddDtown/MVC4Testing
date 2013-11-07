using System;
using System.Collections.Generic;
using MyCompany.Web.Mvc.Downloaders;

namespace MyCompany.Web.Mvc.REST.Downloaders
{
    public interface IDownloader
    {
        int RequestTimeout { get; set; }
        DownloaderResponse GetResponse(Uri uri);
        DownloaderResponse GetResponse(Uri uri, string cacheKey, CacheSelector cacheSelector);
        DownloaderResponse GetResponse(Uri uri, string cacheKey, CacheSelector cacheSelector, HttpRequestMethod method, Dictionary<string, string> parameters);
    }
}

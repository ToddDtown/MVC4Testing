using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Couchbase;
using Couchbase.Extensions;
using MyCompany.Web.Mvc.Downloaders;

namespace MyCompany.Web.Mvc.REST.Downloaders
{
    public class Downloader<T> : IDownloader
    {
        private readonly CouchbaseClient _couchbaseClientInstance;
        private readonly bool _useCache = false;

        public int RequestTimeout { get; set; }

        public Downloader() : this(false)
        {
            
        }

        public Downloader(bool useCache)
        {
            _useCache = useCache;
            if (_useCache)
            {
                if (_couchbaseClientInstance == null)
                    _couchbaseClientInstance = new CouchbaseClient();
            }
        }

        public DownloaderResponse GetResponse(Uri uri)
        {
            return GetResponse(uri, null, CacheSelector.Unknown, HttpRequestMethod.GET, null);
        }

        public DownloaderResponse GetResponse(Uri uri, string cacheKey, CacheSelector cacheSelector)
        {
            return GetResponse(uri, cacheKey, cacheSelector, HttpRequestMethod.GET, null);
        }

        public DownloaderResponse GetResponse(Uri uri, string cacheKey, CacheSelector cacheSelector, HttpRequestMethod method, Dictionary<string, string> parameters)
        {
            return _useCache ? TryCache(uri, cacheKey, cacheSelector) : TryHttp(uri, method, parameters);
        }

        private DownloaderResponse TryCache(Uri uri, string cacheKey, CacheSelector cacheSelector)
        {
            if (_couchbaseClientInstance != null)
            {
                var cachedContent = _couchbaseClientInstance.Get(cacheKey);
                if (cachedContent != null)
                {
                    return new DownloaderResponse(cachedContent.ToString());
                }
            }
        }

        private DownloaderResponse TryHttp(Uri uri, HttpRequestMethod method, Dictionary<string, string> parameters)
        {
            var request = WebRequest.Create(uri);
            request.Method = ((object)method).ToString();

            if (RequestTimeout > 0)
                request.Timeout = RequestTimeout;

            if (method == HttpRequestMethod.POST)
                AddPOSTParameters(request, parameters);

            HttpWebResponse httpWebResponse;
            DownloaderResponse downloaderResponse;

            try
            {
                
                httpWebResponse = (HttpWebResponse)request.GetResponse();
                var buffer = new byte[4096];
                byte[] responseBytes;
                using (var responseStream = httpWebResponse.GetResponseStream())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        int count;
                        while ((count = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                            memoryStream.Write(buffer, 0, count);
                        responseBytes = memoryStream.ToArray();
                    }
                }

                downloaderResponse = new DownloaderResponse(httpWebResponse.StatusCode, httpWebResponse.ContentType, responseBytes);

            }
            catch (WebException ex)
            {
                if (ex.Response == null) throw;

                httpWebResponse = (HttpWebResponse)ex.Response;
                
                var response = string.Empty;
                using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                downloaderResponse = new DownloaderResponse(httpWebResponse.StatusCode, httpWebResponse.ContentType, response);
            }

            if (httpWebResponse == null || downloaderResponse == null)
                throw new NullReferenceException("Response was null with status code of 200");

            return downloaderResponse;
        }

        private static void AddPOSTParameters(WebRequest request, Dictionary<string, string> parameters)
        {
            var encoding = (Encoding)new UTF8Encoding(false);

            var x = parameters.Aggregate(string.Empty, (keyString, pair) => keyString + "&" + pair.Key + "=" + pair.Value);

            var queryStringParams = parameters.Aggregate(string.Empty, (keyString, pair) => keyString + "&" + pair.Key + "=" + pair.Value).Remove(0, 1);

            request.ContentLength = (long)Encoding.UTF8.GetByteCount(queryStringParams);
            request.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new StreamWriter(request.GetRequestStream(), encoding))
            {
                streamWriter.Write(queryStringParams);
            }
        }
    }
}

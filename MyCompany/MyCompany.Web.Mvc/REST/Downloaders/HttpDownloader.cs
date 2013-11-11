using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MyCompany.Web.Mvc.REST.Downloaders
{
    public class HttpDownloader : IDownloader
    {
        public int RequestTimeout { get; set; }

        public DownloaderResponse GetResponse(Uri uri)
        {
            return GetResponse(uri, HttpRequestMethod.GET, null);
        }

        public DownloaderResponse GetResponse(Uri uri, HttpRequestMethod method, Dictionary<string, string> parameters)
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

                string response;
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

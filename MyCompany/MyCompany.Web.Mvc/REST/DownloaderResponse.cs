using System;
using System.IO;
using System.Net;

namespace MyCompany.Web.Mvc.Downloaders
{
    [Serializable]
    public class DownloaderResponse
    {
        private string _responseString;

        public string ResponseString
        {
            get
            {
                if (string.IsNullOrEmpty(this._responseString))
                {
                    using (MemoryStream memoryStream = new MemoryStream(this.ResponseBytes))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)memoryStream))
                            this._responseString = streamReader.ReadToEnd();
                    }
                }
                return this._responseString;
            }
            set { _responseString = value; }
        }

        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
        public byte[] ResponseBytes { get; set; }

        public DownloaderResponse(string response)
        {
            ResponseString = response;
        }

        public DownloaderResponse(HttpStatusCode statusCode, string contentType, string response)
        {
            StatusCode = statusCode;
            ContentType = contentType;
            ResponseString = response;
        }

        public DownloaderResponse(HttpStatusCode statusCode, string contentType, byte[] responseBytes)
        {
            StatusCode = statusCode;
            ContentType = contentType;
            ResponseBytes = responseBytes;
        }
    }


}

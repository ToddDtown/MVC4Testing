using System;
using MyCompany.Web.Mvc.Models;
using MyCompany.Web.Mvc.REST.Downloaders;

namespace MyCompany.Web.Mvc.Services
{
    public class BazaarVoiceService : IBazaarVoiceService
    {
        private readonly IDownloader _downloader;

        public BazaarVoiceService(IDownloader downloader)
        {
            _downloader = downloader;
        }

        public BazaarVoiceReviews GetReviews(string productId)
        {
            var response = _downloader.GetResponse(new Uri("http://www.cnn.com"));

            return new BazaarVoiceReviews();
        }
    }
}

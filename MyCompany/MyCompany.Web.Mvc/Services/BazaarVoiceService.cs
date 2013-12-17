using System;
using Couchbase;
using MyCompany.Web.Mvc.Models;
using MyCompany.Web.Mvc.REST.Downloaders;

namespace MyCompany.Web.Mvc.Services
{
    public class BazaarVoiceService : IBazaarVoiceService
    {
        private readonly IDownloader _downloader;
        private readonly ICouchbaseClient _couchbaseClient;

        public BazaarVoiceService(IDownloader downloader, ICouchbaseClient couchbaseClient)
        {
            _downloader = downloader;
            _couchbaseClient = couchbaseClient;
        }

        public BazaarVoiceReviews GetReviews(string productId)
        {
            var response = _downloader.GetResponse(new Uri("http://www.cnn.com"));

            return new BazaarVoiceReviews();
        }
    }
}

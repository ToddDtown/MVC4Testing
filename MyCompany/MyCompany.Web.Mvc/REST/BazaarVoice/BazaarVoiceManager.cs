using System;
using System.Configuration;
using Couchbase;
using Enyim.Caching.Memcached;
using MyCompany.Web.Mvc.Queries;
using MyCompany.Web.Mvc.REST.Downloaders;

namespace MyCompany.Web.Mvc.REST.BazaarVoice
{
    public class BazaarVoiceManager : IBazaarVoiceManager
    {
        private IDownloader _downloader;
        private ICouchbaseClient _couchbaseClient;
        public string Response { get; set; }

        public BazaarVoiceManager()
        {
            if (_downloader == null)
                _downloader = new HttpDownloader();

            if (_couchbaseClient == null) 
                _couchbaseClient = new CouchbaseClient();
        }

        public string GetRatings(string productId, int limit = 10, string sort = "Rating:desc")
        {
            var cachedRating = CachedRating(productId);

            if (cachedRating != null)
                return ((DownloaderResponse) cachedRating).ResponseString;
            
            var bazaarVoiceQuery = new WebBazaarVoiceQuery
            {
                ApiVersion = "5.4",
                PassKey = ConfigurationManager.AppSettings["BazaarVoiceKey"],
                Filter = string.Empty,
                ProductId = productId,
                HasComments = true,
                Sort = sort,
                Limit = limit
            };

            var uri = new Uri(bazaarVoiceQuery.ToString());
            var response = _downloader.GetResponse(uri);

            _couchbaseClient.Store(StoreMode.Set, productId, response, DateTime.Now.AddSeconds(30));

            return response.ResponseString;
        }

        private object CachedRating(string productId)
        {
            return _couchbaseClient != null ? _couchbaseClient.Get(productId) : null;
        }
    }
}

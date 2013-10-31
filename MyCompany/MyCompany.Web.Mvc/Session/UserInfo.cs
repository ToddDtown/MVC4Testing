using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyCompany.Web.Mvc.Session
{
    public class UserInfo
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("favorite_skus")]
        public Dictionary<string, string> FavoriteSkus { get; set; }

        [JsonProperty("cart_id")]
        public int CartId { get; set; }

        [JsonProperty("cart_items")]
        public Dictionary<string, string> CartItems { get; set; }

        [JsonProperty("last_login")]
        public DateTime LastLogin { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}

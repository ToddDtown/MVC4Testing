using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Enyim.Caching.Memcached;
using MyCompany.Web.Mvc.Caching;
using Newtonsoft.Json;
using NLog.Internal;
using System.Configuration;

namespace MyCompany.Web.Mvc.Controllers
{
    public class UserInfoTestController : BaseController
    {
        private readonly string _key = "user-2222";
        private readonly string _type = "UserInfo";
        private readonly string _firstName = "Dexter";
        private readonly string _lastName = "Morgan";

        public ActionResult Get()
        {
            var x =
            @"{
                ""ViewerUrls"": {
                                    
                                },
                ""last_name"": ""Doe"",
                ""favorite_skus"": { ""SKU1"": ""ABC1"", ""SKU2"": ""ABC2"", ""SKU3"": ""ABC3"" },
                ""cart_id"": 123456789,
                ""cart_items"": { ""item1"": ""1111111"", ""item2"": ""2222222"", ""item3"": ""3333333"", ""item4"": ""4444444"", ""item5"": ""5555555"" },
                ""last_login"": ""1/1/2013 12:14:098""
            }";


            var userInfo = new UserInfo
            {
                FirstName = _firstName,
                LastName = _lastName,
                FavoriteSkus = new Dictionary<string, string>
                {
                    {"SKU1", "ABC1"},
                    {"SKU2", "ABC2"},
                    {"SKU3", "ABC3"}
                },
                CartId = 11111,
                CartItems = new Dictionary<string, string>
                {
                    {"Item1", "111111"},
                    {"Item2", "222222"},
                    {"Item3", "333333"},
                    {"Item4", "444444"}
                },
                LastLogin = DateTime.Now,
                Type = _type
            };

            //StoreMode mode;
            //if (CouchbaseManager.Get(_key) != null)
            //    mode = StoreMode.Set;
            //else
            //    mode = StoreMode.Add;

            CouchbaseManager.StoreJson(_key, userInfo, StoreMode.Set);

            //var imageViewerInfo = new ImageViewerInfo();
            //imageViewerInfo.ViewerImageUrls = new List<ImageUrl>();

            //var imageUrl = new ImageUrl {Url = "http://www.cdn.com/image1.jpg", Origin = "IR"};
            //imageViewerInfo.ViewerImageUrls.Add(imageUrl);

            //imageUrl = new ImageUrl { Url = "http://www.cdn.com/image2.jpg", Origin = "IS" };
            //imageViewerInfo.ViewerImageUrls.Add(imageUrl);

            //imageUrl = new ImageUrl { Url = "http://www.cdn.com/image3.jpg", Origin = "IS" };
            //imageViewerInfo.ViewerImageUrls.Add(imageUrl);

            //imageUrl = new ImageUrl { Url = "http://www.cdn.com/image4.jpg", Origin = "IR" };
            //imageViewerInfo.ViewerImageUrls.Add(imageUrl);

            //imageUrl = new ImageUrl { Url = "http://www.cdn.com/image5.jpg", Origin = "IS" };
            //imageViewerInfo.ViewerImageUrls.Add(imageUrl);
            
            //CouchbaseManager.StoreJson(_key, imageViewerInfo, StoreMode.Set);
            
            return View("UserInfoTest");
        }
    }
}

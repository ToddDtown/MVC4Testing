using System;
using System.Collections.Generic;
using MyCompany.BusinessModel.Models;

namespace MyCompany.Web.Mvc.Models.ModelBuilders 
{
    public static class HomeModelBuilder
    {
        public static HomeModel CreateModel()
        {
            var skus = new Dictionary<int, string>()
            {
                {1, "headlight-black-proj-2010"},
                {2, "sct-4bank-custom-9498gt"},
                {3, "cdc-dynamite-sticks"},
                {4, "steeda-adjustable-clutch-kit-8395"},
                {5, "mustang-smoked-headlights-8793"},
                {6, "16x8-4lug-cobra-r-chrome"}
            };

            var rnd = new Random();
            var num = rnd.Next(1, 6);
            var model = new HomeModel
            {
                SKU = skus[num]
            };
            return model;
        }
    }
}

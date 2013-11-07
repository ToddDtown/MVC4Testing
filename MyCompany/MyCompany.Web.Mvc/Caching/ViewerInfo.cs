using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyCompany.Web.Mvc.Caching
{
    public class ImageViewerInfo
    {
        [JsonProperty("ImageUrls")]
        public List<ImageUrl> ViewerImageUrls;
    }

    [JsonObject("ImageUrl")]
    public class ImageUrl
    {
        [JsonProperty("Url")]
        public string Url { get; set; }

        [JsonProperty("Origin")]
        public string Origin { get; set; }
    }
}

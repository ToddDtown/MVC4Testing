using System.Collections.Generic;

namespace MyCompany.Web.Mvc.Models
{
    public class ImageViewer
    {
        public List<Image> ProductImages { get; set; }
        public List<YearColors> YearColors { get; set; }
    }

    public class Image
    {
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public bool IsColor { get; set; }
    }

    public class YearColors
    {
        public string Year { get; set; }
        public List<ColorInfo> Colors { get; set; }
    }

    public class ColorInfo
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Hex { get; set; }
        public string Rgb { get; set; }
    }
}

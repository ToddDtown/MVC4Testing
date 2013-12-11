using System;
using System.Xml.Serialization;

namespace MyCompany.Web.WebServices.Models
{
    [Serializable]
    public class Transaction
    {
        [XmlElementAttribute]
        public string ExactID { get; set; }

        public void Do()
        {
            
        }
    }
}
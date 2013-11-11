namespace MyCompany.Web.Mvc.Models
{
    public static class BazaarVoiceModelBuilder
    {
        public static BazaarVoiceModel CreateModel()
        {
            var model = new BazaarVoiceModel();

            model.ProductId = "headlight-black-proj-2010";
            
            return model;
        }
    }
}

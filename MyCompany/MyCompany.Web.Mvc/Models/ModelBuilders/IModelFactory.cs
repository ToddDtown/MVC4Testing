namespace MyCompany.Web.Mvc.Models.ModelBuilders
{
    public interface IModelFactory
    {
        BazaarVoiceModel CreateBazaarVoiceModel(string bvResponse);
    }
}

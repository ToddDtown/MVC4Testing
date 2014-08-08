using Microsoft.AspNet.Membership.OpenAuth;

namespace MyCompany.Web.UI.App_Start
{
    public class AuthConfig
    {
        public static void RegisterOpenAuth()
        {
            OpenAuth.ConnectionString = "MembershipSecurity";
        }
    }
}
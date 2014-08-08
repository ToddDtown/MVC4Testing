using System.Web.Mvc;
using System.Web.Security;
using MyCompany.Web.Mvc.Models;

namespace MyCompany.Web.Mvc.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            var account = new AccountModel();
            return View("Account", account);
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginToAccount(FormCollection fields)
        {
            var loginSuccessful = Membership.ValidateUser(fields["ussername"], fields["password"]);

            if (loginSuccessful)
            {
                return RedirectPermanent("/Account/Index");
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection fields)
        {
            MembershipCreateStatus status;
            Membership.CreateUser(fields["username"], fields["password"], fields["email"], string.Empty, string.Empty, true, out status);

            if (status == MembershipCreateStatus.Success)
            {
                return RedirectPermanent("Login");
            }

            return Content("");
        }
    }
}

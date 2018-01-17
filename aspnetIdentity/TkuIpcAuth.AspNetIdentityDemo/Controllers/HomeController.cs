using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using TkuIpcAuth.AspNetIdentity;

namespace TkuIpcAuth.AspNetIdentityDemo.Controllers {
    public class HomeController : Controller {
        public UserManager<TkUser> userManager;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            base.Initialize(requestContext);
            userManager = HttpContext.GetOwinContext().Get<UserManager<TkUser>>();            
        }

        [TkuAuthorize()]
        public ActionResult Index(string provider = "") {
            var user = userManager.FindByName(User.Identity.Name);
                        
            ViewData["uid"]      = User.Identity.Name;
            ViewData["provider"] = provider;

            return View();
        }

        [TkuAuthorize(Roles="SysAdmins")]
        public ActionResult Vip() { 
            return View();
        }
    }
}
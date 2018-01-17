using System;
using System.Web.Configuration;
using System.Web.Mvc;

using TkuIpcAuth.AspNetIdentity;

namespace TkuIpcAuth.AspNetIdentityDemo.Controllers {
    public class TkuController : Controller {
        public ActionResult Index(string sKey, string provider, string host) {
            string tkuCallback = WebConfigurationManager.AppSettings["tku:callback"];
            string sso_userid  = Request.Headers["sso_userid"];
            string sIV         = Guid.NewGuid().ToString().Substring(0, 8);
            string uID         = TkuIpcAuthenticationHelper.Encrypt(sso_userid, sKey, sIV);
            
            ViewData["callback"] = "http://" + host + tkuCallback + "?provider=" + provider + "&uID=" + uID + "&sIV=" + sIV;

            return View();
        }
    }
}
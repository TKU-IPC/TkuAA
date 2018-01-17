using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Routing;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using TkuIpcAuth.AspNetIdentity;

namespace TkuIpcAuth.AspNetIdentityDemo.Controllers {
    public class AccountController : Controller {
        public UserManager<TkUser> userManager;
        public SignInManager<TkUser, string> signInManager;

        string tkuSKeyNm = "SSOTOKEN";
        string tkuSso    = WebConfigurationManager.AppSettings["tku:sso"];
                
        protected override void Initialize(RequestContext requestContext) {
            base.Initialize(requestContext);
                        
            userManager   = HttpContext.GetOwinContext().Get<UserManager<TkUser>>();
            signInManager = HttpContext.GetOwinContext().Get<SignInManager<TkUser, string>>();
        }

        public ActionResult GetProviderView(string provider) {
            ActionResult actionResult = HttpNotFound();

            TempData.Clear();
                                    
            switch (provider) { 
                case "Local":       
                    actionResult = PartialView("FormForLocalAuth");
                    break;

                case "Mix":
                    var sKey = "_" + Guid.NewGuid().ToString().Substring(0, 7);
                    var host = Request.Url.Host + (Request.Url.IsDefaultPort ? string.Empty : ":" + Request.Url.Port.ToString());
                    
                    TempData[tkuSKeyNm] = sKey;
                    ViewData["login"]   = tkuSso + "?sKey=" + sKey + "&provider=" + provider + "&host=" + host + "&embed=yes";
                                        
                    actionResult = PartialView("FormForMixAuth");
                    break;
            }

            return actionResult;
        }
                
        public ActionResult RedirectToProvider(string provider) {
            ActionResult actionResult = HttpNotFound();

            TempData.Clear();

            switch (provider) { 
                case "Facebook":
                case "Google":
                    var prop = new AuthenticationProperties() {
                        RedirectUri = Url.Action("ExternalCallback", new { provider })
                    };

                    signInManager.AuthenticationManager.Challenge(prop, provider);

                    actionResult = new HttpUnauthorizedResult();
                    break;
                
                case "Tku":
                    var sKey = "_" + Guid.NewGuid().ToString().Substring(0, 7);
                    var host = Request.Url.Host + (Request.Url.IsDefaultPort ? string.Empty : ":" + Request.Url.Port.ToString());

                    TempData[tkuSKeyNm] = sKey;
                                        
                    actionResult = Redirect(tkuSso + "?sKey=" + sKey + "&provider=" + provider + "&host=" + host);
                    break;

                default:
                    break;
            }

            return actionResult;
        }
        
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Login")]
        public async Task<ActionResult> LocalCallback(string userName, string password) {
            ActionResult actionResult = View();
                        
            var signInStatus = await signInManager.PasswordSignInAsync(userName, password, false, false);
            
            if (signInStatus == SignInStatus.Success) {
                actionResult = RedirectToAction("Index", "Home", new { provider = "Local" });
            }
            
            return actionResult;
        }
                
        public async Task<ActionResult> ExternalCallback(string provider, string uID = "", string sIV = "") {
            ActionResult actionResult = null;
            SignInStatus signinStatus = SignInStatus.RequiresVerification;

            switch (provider) {
                case "Facebook":
                case "Google":
                    //fix access denied issue when run on web1.ais
                    //https://stackoverflow.com/questions/38751641/app-redirects-to-account-accessdenied-on-adding-oauth
                    Response.Cookies.Remove("Identity.External");
                    
                    var loginInfo = await signInManager.AuthenticationManager.GetExternalLoginInfoAsync();                    
                    signinStatus = await signInManager.ExternalSignInAsync(loginInfo, false);

                    break;

                case "Mix":
                case "Tku":
                    string sKey = TempData[tkuSKeyNm].ToString();
                                
                    try { 
                        string userName = TkuIpcAuthenticationHelper.Decrypt(uID, sKey, sIV);
                        var identity    = await TkuIpcAuthenticationHelper.GetIdentityAsync(userName, userManager);
                        
                        if (identity != null) {
                            signInManager.AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
                            signinStatus = SignInStatus.Success;
                        }
                    }
                    catch { 
                        signinStatus = SignInStatus.Failure;
                    }

                    break;

                default:
                    break;
            }

            if (signinStatus == SignInStatus.Success) {
                actionResult = RedirectToAction("Index", "Home", new { provider });
            }

            return actionResult;
        }

        public ActionResult Logout() {
            signInManager.AuthenticationManager.SignOut();
            return View();
        }
    }
}
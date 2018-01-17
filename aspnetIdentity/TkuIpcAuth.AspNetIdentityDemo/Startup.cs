using System;
using System.Threading.Tasks;
using System.Web.Configuration;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;

using TkuIpcAuth.AspNetIdentity;

[assembly: OwinStartup(typeof(TkuIpcAuth.AspNetIdentityDemo.Startup))]

namespace TkuIpcAuth.AspNetIdentityDemo {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            app.CreatePerOwinContext(() => new TkUserDbContext("name=IdentityDb"));
            app.CreatePerOwinContext<TkUserStore>((opt, ctx) => new TkUserStore(ctx.Get<TkUserDbContext>()));            
            app.CreatePerOwinContext<UserManager<TkUser>>((opt, cont) => new UserManager<TkUser>(cont.Get<TkUserStore>()));            
            //app.CreatePerOwinContext<TkRoleStore>((opt, ctx) => new TkRoleStore(ctx.Get<TkUserDbContext>()));            
            //app.CreatePerOwinContext<RoleManager<TkRole>>((opt, cont) => new RoleManager<TkRole>(cont.Get<TkRoleStore>()));            
            app.CreatePerOwinContext<SignInManager<TkUser, string>>((opt, cont) =>  new SignInManager<TkUser, string>(cont.Get<UserManager<TkUser>>(), cont.Authentication));
                                                            
            app.UseCookieAuthentication(new CookieAuthenticationOptions() {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie/*,
                LoginPath = new PathString("/Account/Login")*/
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
                        
            app.UseFacebookAuthentication(new FacebookAuthenticationOptions() {
                AppId     = WebConfigurationManager.AppSettings["facebook:id"],
                AppSecret = WebConfigurationManager.AppSettings["facebook:secret"],
                Caption   = "Facebook"
            });

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions() {
                ClientId     = WebConfigurationManager.AppSettings["google:id"],
                ClientSecret = WebConfigurationManager.AppSettings["google:secret"],
                Caption      = "Google"
            });
        }
    }
}

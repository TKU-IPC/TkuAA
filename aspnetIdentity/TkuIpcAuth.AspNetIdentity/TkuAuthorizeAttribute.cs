using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace TkuIpcAuth.AspNetIdentity {
    // 找不到方法把 Roles 放入 HttpContext.User，導致 user.IsInRole 無法通過，故自行撰寫
    public class TkuAuthorizeAttribute : AuthorizeAttribute {
        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            bool authenticated = httpContext.Request.IsAuthenticated;
                        
            var userStore = httpContext.GetOwinContext().Get<TkUserStore>();
            var user      = userStore.FindByName(httpContext.User.Identity.Name);
            
            // check roles
            if (authenticated && !string.IsNullOrEmpty(Roles)) {                
                // 使用者是否具有該控制器所宣稱的角色
                var userRoles = user.TkUserRoles.Select(ur => ur.RoleId.Trim());                
                authenticated = RolesComparer.HasIt(Roles, userRoles);
            }
           
            return authenticated;
        }
    }    

    public class RolesComparer {
        public static bool HasIt(string rolesComparer, IEnumerable<string> userRoles) {
            bool result = false;

            if (!string.IsNullOrEmpty(rolesComparer)) {
                var roles = rolesComparer.Split(',');

                // 去除 roles 的空白字元
                for (int i = 0; i < roles.Count(); i++) { 
                    roles[i] = roles[i].Trim();
                }

                result = roles.Intersect(userRoles).Count() > 0;
            }
            
            return result;
        }
    }
}
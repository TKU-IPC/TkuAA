using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace TkuIpcAuth.AspNetIdentity {   
    public partial class TkUser : IUser<string> {
        public TkUser() { 
            TkClaims = new List<TkClaim>();
            TkUserLoginInfos = new List<TkUserLoginInfo>();
            TkUserRoles = new List<TkUserRole>();
        }

        public string Id { get; set; }          //inherit from IUser, must be unique
        public string UserName { get; set; }    //inherit from IUser, must be unique
        public string RealName { get; set; }
        public string PasswordHash { get; set; }
        public string UserPswd { get; set; }
        public string FutmDept { get; set; }
        public string PhoneExt { get; set; }
        public string UserDept { get; set; }
        public string UserDeptInq { get; set; }

        public virtual ICollection<TkClaim> TkClaims { get; set; }
        public virtual ICollection<TkUserLoginInfo> TkUserLoginInfos { get; set; }
        public virtual ICollection<TkUserRole> TkUserRoles { get; set; }
    }
}

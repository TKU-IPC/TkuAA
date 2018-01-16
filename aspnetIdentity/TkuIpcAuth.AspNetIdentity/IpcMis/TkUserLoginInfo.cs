using System;

namespace TkuIpcAuth.AspNetIdentity {
    public partial class TkUserLoginInfo {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }
    }
}

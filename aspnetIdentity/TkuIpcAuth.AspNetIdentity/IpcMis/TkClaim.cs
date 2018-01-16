using System;

namespace TkuIpcAuth.AspNetIdentity {
    public partial class TkClaim {        
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string UserId { get; set; }
    }
}

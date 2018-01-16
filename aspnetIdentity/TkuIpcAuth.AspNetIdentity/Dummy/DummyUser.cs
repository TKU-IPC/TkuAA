using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

namespace TkuIpcAuth.AspNetIdentity {
    public class DummyUser : IUser {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}

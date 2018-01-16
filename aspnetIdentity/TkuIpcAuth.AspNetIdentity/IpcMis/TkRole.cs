using System;
using Microsoft.AspNet.Identity;

namespace TkuIpcAuth.AspNetIdentity {   
    public partial class TkRole : IRole<string> {        

        public string Id { get; set; }      //inherit from IRole, must be unique
        public string Name { get; set; }    //inherit from IRole, must be unique
        public string RoleName { get; set; }        
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer.Utilities;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace TkuIpcAuth.AspNetIdentity {
    public class TkRoleStore : IRoleStore<TkRole> {

        TkUserDbContext context;
        TkRole role;
                
        DbSet<TkRole> roles;
        
        private void init(TkUserDbContext context) {
            this.context = context;
                        
            roles = context.Set<TkRole>();           
        }

        public TkRoleStore() {
            init(new TkUserDbContext());
        }

        public TkRoleStore(TkUserDbContext context) {
            init(context);
        }
                
        public Task CreateAsync(TkRole role) {
            return Task.FromResult(0);
        }

        public Task DeleteAsync(TkRole role) {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(TkRole role) {
            return Task.FromResult(0);
        }

        public Task<TkRole> FindByIdAsync(string roleId) {
            throw new NotImplementedException();
        }

        public Task<TkRole> FindByNameAsync(string roleName) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            context.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer.Utilities;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace TkuIpcAuth.AspNetIdentity {
    public class TkUserStore : IUserStore<TkUser>, 
                               IUserPasswordStore<TkUser>,
                               IUserLockoutStore<TkUser, string>,
                               IUserTwoFactorStore<TkUser, string>,
                               IUserClaimStore<TkUser, string>,
                               IUserLoginStore<TkUser, string>,
                               IUserRoleStore<TkUser> {

        TkUserDbContext context;
        TkUser user;

        DbSet<TkClaim> claims;
        DbSet<TkRole> roles;
        DbSet<TkUser> users;
        DbSet<TkUserLoginInfo> infos;
        DbSet<TkUserRole> userRoles;

        private void init(TkUserDbContext context) {
            this.context = context;

            claims    = context.Set<TkClaim>();
            roles     = context.Set<TkRole>();
            users     = context.Set<TkUser>();
            infos     = context.Set<TkUserLoginInfo>();
            userRoles = context.Set<TkUserRole>();
        }

        public TkUserStore() {
            init(new TkUserDbContext());
        }

        public TkUserStore(TkUserDbContext context) {
            init(context);
        }
               
        public Task CreateAsync(TkUser user) {
            return Task.FromResult(0);
        }

        public Task DeleteAsync(TkUser user) {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(TkUser user) {
            return Task.FromResult(0);
        }

        public async Task<TkUser> FindByIdAsync(string emplNo) {
            if (user == null) {
                user = await users.Include(m => m.TkClaims)
                                  .Include(m => m.TkUserLoginInfos)
                                  .Include(m => m.TkUserRoles)
                                  .FirstOrDefaultAsync(m => m.Id == emplNo)
                                  .WithCurrentCulture();
            }

            return user;
        }

        public async Task<TkUser> FindByNameAsync(string emplNo) {
            if (user == null) { 
                user = await users.Include(m => m.TkClaims)
                                  .Include(m => m.TkUserLoginInfos)
                                  .Include(m => m.TkUserRoles)
                                  .FirstOrDefaultAsync(m => m.UserName == emplNo)
                                  .WithCurrentCulture();
            }

            return user;
        }
        
        public TkUser FindByName(string emplNo) {
            if (user == null) { 
                user = users.Include(m => m.TkClaims)
                            .Include(m => m.TkUserLoginInfos)
                            .Include(m => m.TkUserRoles)
                            .FirstOrDefault(m => m.UserName == emplNo);
            }

            return user;
        }

        public void Dispose() {
            context.Dispose();
        }

        public Task<string> GetPasswordHashAsync(TkUser user) {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TkUser user) {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(TkUser user, string password) {
            user.PasswordHash = (new PasswordHasher()).HashPassword(password);
            context.TkUsers.Attach(user);

            return context.SaveChangesAsync();
        }

        /*****************************************************************************************/
        //目前尚未使用 IUserLockoutStore & IUserTwoFactorStore 的功能，但 UserManager & SignInManager 會呼叫對應的方法，故仍須實作
        public Task<int> GetAccessFailedCountAsync(TkUser user) {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(TkUser user) {
            return Task.FromResult(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TkUser user) {
            return Task.FromResult(DateTimeOffset.MaxValue);
        }

        public Task<int> IncrementAccessFailedCountAsync(TkUser user) {
            return Task.FromResult(3);
        }

        public Task ResetAccessFailedCountAsync(TkUser user) {
            return Task.FromResult(0);
        }

        public Task SetLockoutEnabledAsync(TkUser user, bool enabled) {
            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(TkUser user, DateTimeOffset lockoutEnd) {
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(TkUser user) {
            return Task.FromResult(false);
        }

        public Task SetTwoFactorEnabledAsync(TkUser user, bool enabled) {
            return Task.FromResult(0);
        }
        /*****************************************************************************************/

        public Task AddClaimAsync(TkUser user, Claim claim) {
            return Task.FromResult(0);
        }

        public Task<IList<Claim>> GetClaimsAsync(TkUser user) {            
            IList<Claim> result = user.TkClaims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            return Task.FromResult(result);
        }

        public Task RemoveClaimAsync(TkUser user, Claim claim) {
            return Task.FromResult(0);
        }

        public Task AddLoginAsync(TkUser user, UserLoginInfo login) {
            return Task.FromResult(0);
        }

        public async Task<TkUser> FindAsync(UserLoginInfo login) {
            if (user == null) { 
                var provider = login.LoginProvider;
                var key      = login.ProviderKey;

                var userLoginInfo = await infos.FirstOrDefaultAsync(i => i.LoginProvider == provider && i.ProviderKey == key);

                if (userLoginInfo != null) { 
                    var userId = userLoginInfo.UserId;

                    user = await users.Include(m => m.TkClaims)
                                      .Include(m => m.TkUserLoginInfos)
                                      .Include(m => m.TkUserRoles)
                                      .FirstOrDefaultAsync(m => m.Id == userId)
                                      .WithCurrentCulture();
                }
            }

            return user;
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TkUser user) {
            IList<UserLoginInfo> result = user.TkUserLoginInfos.Select(c => new UserLoginInfo(c.LoginProvider, c.ProviderKey)).ToList();
            return Task.FromResult(result);
        }

        public Task RemoveLoginAsync(TkUser user, UserLoginInfo login) {
            return Task.FromResult(0);
        }

        public Task AddToRoleAsync(TkUser user, string roleName) {
            return Task.FromResult(0);
        }

        public async Task<IList<string>> GetRolesAsync(TkUser user) {
            var userId = user.Id;

            var query = from ur in userRoles
                        where ur.UserId == userId
                        join r in roles on ur.RoleId equals r.Id
                        select r.Name;

            return await query.ToListAsync().WithCurrentCulture();
        }

        public async Task<bool> IsInRoleAsync(TkUser user, string roleName) {
            var role = await roles.SingleOrDefaultAsync(r => r.RoleName == roleName).WithCurrentCulture();

            if (role != null) { 
                var userId = user.Id;
                var roleId = role.Id;

                return await userRoles.AnyAsync(ur => ur.RoleId == roleId && ur.UserId == userId).WithCurrentCulture();
            }

            return false;
        }

        public Task RemoveFromRoleAsync(TkUser user, string roleName) {
            throw new NotImplementedException();
        }
    }
}

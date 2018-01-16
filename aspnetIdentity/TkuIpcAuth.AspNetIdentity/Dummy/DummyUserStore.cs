using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

namespace TkuIpcAuth.AspNetIdentity {
    public class DummyUserStore : IUserStore<DummyUser>,
                                  IUserPasswordStore<DummyUser>,
                                  IUserLockoutStore<DummyUser, string>,
                                  IUserTwoFactorStore<DummyUser, string>,
                                  IUserClaimStore<DummyUser, string>,
                                  IUserLoginStore<DummyUser, string> {

        DummyUserDbContext context;
        
        public DummyUserStore() {
            context = new DummyUserDbContext();
        }
                
        public Task CreateAsync(DummyUser user) {
            return Task.FromResult(0);
        }

        public Task DeleteAsync(DummyUser user) {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(DummyUser user) {
            return Task.FromResult(0);
        }

        public Task<DummyUser> FindByIdAsync(string userId) {
            var user = new DummyUser() {
                Id = userId,
                UserName = "noname"
            };

            return Task.FromResult(user);
        }

        public Task<DummyUser> FindByNameAsync(string userName) {
            var user = new DummyUser() {
                Id = Guid.NewGuid().ToString(),
                UserName = userName
            };

            return Task.FromResult(user);
        }
        
        public void Dispose() {
            context.Dispose();
        }

        public Task<string> GetPasswordHashAsync(DummyUser user) {
            var hasher   = new PasswordHasher();
            var password = "password";
            var passwordHashed = hasher.HashPassword(password);
                        
            return Task.FromResult(passwordHashed);
        }

        public Task<bool> HasPasswordAsync(DummyUser user) {
            return Task.FromResult(false);
        }

        public Task SetPasswordHashAsync(DummyUser user, string passwordHash) {
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(DummyUser user) {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(DummyUser user) {
            return Task.FromResult(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(DummyUser user) {
            return Task.FromResult(DateTimeOffset.MaxValue);
        }

        public Task<int> IncrementAccessFailedCountAsync(DummyUser user) {
            return Task.FromResult(3);
        }

        public Task ResetAccessFailedCountAsync(DummyUser user) {
            return Task.FromResult(0);
        }

        public Task SetLockoutEnabledAsync(DummyUser user, bool enabled) {
            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(DummyUser user, DateTimeOffset lockoutEnd) {
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(DummyUser user) {
            return Task.FromResult(false);
        }

        public Task SetTwoFactorEnabledAsync(DummyUser user, bool enabled) {
            return Task.FromResult(0);
        }

        public Task AddClaimAsync(DummyUser user, Claim claim) {
            return Task.FromResult(0);
        }

        public Task<IList<Claim>> GetClaimsAsync(DummyUser user) {
            IList<Claim> claims = new List<Claim>();

            return Task.FromResult(claims);
        }

        public Task RemoveClaimAsync(DummyUser user, Claim claim) {
            return Task.FromResult(0);
        }

        public Task AddLoginAsync(DummyUser user, UserLoginInfo login) {
            return Task.FromResult(0);
        }

        public Task<DummyUser> FindAsync(UserLoginInfo login) {
            var user = new DummyUser() {
                Id = Guid.NewGuid().ToString(),
                UserName = "noname"
            };

            return Task.FromResult(user);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(DummyUser user) {
            IList<UserLoginInfo> loginInfos = new List<UserLoginInfo>();
            
            return Task.FromResult(loginInfos);
        }

        public Task RemoveLoginAsync(DummyUser user, UserLoginInfo login) {
            return Task.FromResult(0);
        }
    }
}

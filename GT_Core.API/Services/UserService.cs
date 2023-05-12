using GT_Core.Domain.Interfaces;
using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GT_Core.API.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext DbContext;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly PasswordHasher<ApplicationUser> PasswordHasher;
        private readonly ITokenService TokenService;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> UserClaimsPrincipalFactory;
        private readonly IAuthorizationService AuthorizationService;
        private readonly IAuthenticationSchemeProvider Schemes;
        private readonly IConfiguration Configuration;

        public UserService(
            IApplicationDbContext _dbContext,
            UserManager<ApplicationUser> _userManager,
            ITokenService _tokenService,
            IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory,
            IAuthorizationService _authorizationService,
            IAuthenticationSchemeProvider _schemes,
            IConfiguration _configuration)
        {
            DbContext = _dbContext;
            UserManager = _userManager;
            PasswordHasher = new PasswordHasher<ApplicationUser>();
            TokenService = _tokenService;
            UserClaimsPrincipalFactory = _userClaimsPrincipalFactory;
            AuthorizationService = _authorizationService;
            Schemes = _schemes;
            Configuration = _configuration;
        }

        public async Task<Result<JwtSecurityToken>> AuthenticateAsync(string _userName, string _password)
        {
            var user = await UserManager.FindByNameAsync(_userName);

            if (user == null || PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, _password) == PasswordVerificationResult.Failed)
            {
                return new Result<JwtSecurityToken>(false);
            }

            var principal = await UserClaimsPrincipalFactory.CreateAsync(user);

            var token = TokenService.GenerateToken(principal);

            return new Result<JwtSecurityToken>(true, token); 
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await UserManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<Result<string>> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await UserManager.CreateAsync(user, password);

            return result.ToApplicationResult(user.Id);
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = UserManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await UserManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = UserManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var principal = await UserClaimsPrincipalFactory.CreateAsync(user);

            var result = await AuthorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result<string>> DeleteUserAsync(string userId)
        {
            var user = UserManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : Result<string>.Success();
        }

        public async Task<Result<string>> DeleteUserAsync(ApplicationUser _user)
        {
            var result = await UserManager.DeleteAsync(_user);

            return result.ToApplicationResult();
        }

        public Task<Result<JwtSecurityToken>> LoginAsync(string _username, string _password)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> AddToRoleAsync(string _role, string _username)
        {
            throw new NotImplementedException();
        }

        Task<Result<string>> IUserService.GetUserNameAsync(string _userId)
        {
            throw new NotImplementedException();
        }

        Task<Result<bool>> IUserService.IsInRoleAsync(string _userId, string _role)
        {
            throw new NotImplementedException();
        }

        Task<Result<bool>> IUserService.AuthorizeAsync(string _userId, string _policyName)
        {
            throw new NotImplementedException();
        }

        #region GENERIC METHODS

        public async Task<Result<ApplicationUser>> Create(ApplicationUser _user, CancellationToken _cancellationToken = default)
        {
            var result = await DbContext.Set<ApplicationUser>().AddAsync(_user, _cancellationToken);

            var changes = await DbContext.SaveChangesAsync(_cancellationToken);

            if (changes > 0)
            {
                return new Result<ApplicationUser>(true, result.Entity);
            }

            return new Result<ApplicationUser>(false, _user);
        }

        public async Task<Result<ApplicationUser>> Read(string _id, CancellationToken _cancellationToken = default)
        {
            ApplicationUser? user = await Task.Run(() =>
            {
                return DbContext.Set<ApplicationUser>().FirstOrDefault(e => e.Id.Equals(_id));
            });

            if (user != null)
            {
                return new Result<ApplicationUser>(true, user);
            }

            return new Result<ApplicationUser>(false, new ApplicationUser());
        }

        public async Task<Result<IEnumerable<ApplicationUser>>> ReadAll(CancellationToken _cancellationToken = default)
        {
            IEnumerable<ApplicationUser> users = await Task.Run(() =>
            {
                return DbContext.Set<ApplicationUser>().ToList();
            });

            return new Result<IEnumerable<ApplicationUser>>(true, users);
        }

        public async Task<Result<ApplicationUser>> Update(ApplicationUser _user, CancellationToken _cancellationToken = default)
        {
            ApplicationUser? user = DbContext.Set<ApplicationUser>().FirstOrDefault(e => e.Id.Equals(_user.Id));

            if (user != null)
            {
                DbContext.Set<ApplicationUser>().Update(user);

                user = _user;

                var changes = await DbContext.SaveChangesAsync(_cancellationToken);

                if (changes > 0)
                {
                    return new Result<ApplicationUser>(true, user);
                }
            }

            return new Result<ApplicationUser>(false, _user);
        }

        public async Task<Result<ApplicationUser>> Delete(string _id, CancellationToken _cancellationToken = default)
        {
            ApplicationUser? user = DbContext.Set<ApplicationUser>().FirstOrDefault(e => e.Id.Equals(_id));

            if (user != null)
            {
                DbContext.Set<ApplicationUser>().Remove(user);

                var changes = await DbContext.SaveChangesAsync(_cancellationToken);

                if (changes > 0)
                {
                    return new Result<ApplicationUser>(true, user);
                }
            }

            return new Result<ApplicationUser>(false, new ApplicationUser());
        }

        #endregion
    }
}

using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GT_Core.API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly PasswordHasher<ApplicationUser> PasswordHasher;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> UserClaimsPrincipalFactory;
        private readonly IAuthorizationService AuthorizationService;
        private readonly IAuthenticationSchemeProvider Schemes;
        private readonly IConfiguration Configuration;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            IAuthenticationSchemeProvider schemes,
            IConfiguration _configuration)
        {
            UserManager = userManager;
            PasswordHasher = new PasswordHasher<ApplicationUser>();
            UserClaimsPrincipalFactory = userClaimsPrincipalFactory;
            AuthorizationService = authorizationService;
            Schemes = schemes;
            Configuration = _configuration;
        }

        public async Task<JWToken> AuthenticateAsync(string _userName, string _password, bool _persistent)
        {
            var user = await UserManager.FindByNameAsync(_userName);

            if (user == null || PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, _password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return GetUserToken(user);
        }

        private JWToken GetUserToken(ApplicationUser _user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                              {
                                new Claim(ClaimTypes.Name, _user.UserName)
                              }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(Configuration["JWT:Lifetime"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JWToken { Token = tokenHandler.WriteToken(token) };
        }

        public Task<bool> AuthorizeAsync(string _userId, string _policyName)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> CreateUserAsync(string _userName, string _password)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> DeleteUserAsync(string _userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(string _userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(string _userId, string _role)
        {
            throw new NotImplementedException();
        }
    }
}

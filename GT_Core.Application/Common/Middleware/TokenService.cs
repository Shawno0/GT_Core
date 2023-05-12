using GT_Core.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GT_Core.Application.Common.Middleware
{
    public class TokenService : ITokenService
    {
        private JwtSecurityToken Token;

        private readonly IConfiguration Configuration;
        private readonly ILogger<TokenService> Logger;

        public TokenService(
            IConfiguration _configuration, 
            ILogger<TokenService> _logger)
        {
            Configuration = _configuration;
            Logger = _logger;
        }

        public JwtSecurityToken GenerateToken(ClaimsPrincipal _principal)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(_principal.Claims),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(Configuration["JWT:Lifetime"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            Token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return Token;
        }

        public JwtSecurityToken GetToken()
        {
            return Token;
        }

        public void SetToken(JwtSecurityToken _token)
        {
            Token = _token;
        }

        public IEnumerable<Claim> GetClaims()
        {
            if (Token == null)
            {
                return Enumerable.Empty<Claim>();
            }

            return Token.Claims;
        }

        public ClaimsIdentity GetIdentity()
        {
            var identity = new ClaimsIdentity(GetClaims(), "Jwt", "name", "role");

            return identity;
        }

        public ClaimsPrincipal GetPrincipal()
        {
            var principal = new ClaimsPrincipal(GetIdentity());

            return principal;
        }

        public bool IsAuthenticated()
        {
            if (Token == null)
            {
                return false;
            }

            return Token.ValidFrom < DateTime.UtcNow && Token.ValidTo > DateTime.UtcNow;
        }
    }
}

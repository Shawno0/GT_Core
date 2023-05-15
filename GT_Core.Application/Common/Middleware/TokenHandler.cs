using GT_Core.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GT_Core.Application.Common.Middleware
{
    public class TokenHandler : ITokenHandler
    {
        private const string AUTHENTICATION_TYPE = "Jwt";
        private const string NAME_TYPE = "name";
        private const string ROLE_TYPE = "role";
        private JwtSecurityToken Token;

        private readonly IConfiguration Configuration;
        private readonly ILogger<TokenHandler> Logger;

        public TokenHandler(
            IConfiguration _configuration, 
            ILogger<TokenHandler> _logger)
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
            return Token ?? new JwtSecurityToken();
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
            var identity = new ClaimsIdentity(GetClaims(), AUTHENTICATION_TYPE, NAME_TYPE, ROLE_TYPE);

            return identity;
        }

        public ClaimsPrincipal GetPrincipal()
        {
            var principal = new ClaimsPrincipal(GetIdentity());

            return principal;
        }

        public string GetHttpHeader()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var result = tokenHandler.WriteToken(GetToken());

            return result;
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

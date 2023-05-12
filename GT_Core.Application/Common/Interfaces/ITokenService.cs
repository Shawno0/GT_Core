﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GT_Core.Application.Common.Interfaces
{
    public interface ITokenService
    {
        public JwtSecurityToken GenerateToken(ClaimsPrincipal _principal);
        public JwtSecurityToken GetToken();
        public void SetToken(JwtSecurityToken _token);
        public IEnumerable<Claim> GetClaims();
        public ClaimsPrincipal GetPrincipal();
        public ClaimsIdentity GetIdentity();
        public bool IsAuthenticated();
    }
}

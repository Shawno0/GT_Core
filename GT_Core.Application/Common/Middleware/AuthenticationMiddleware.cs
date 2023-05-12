using GT_Core.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace GT_Core.Application.Common.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate Next;
        private readonly ITokenService TokenService;
        private readonly ILogger<AuthenticationMiddleware> Logger;

        public AuthenticationMiddleware(
            RequestDelegate _next,
            ITokenService _tokenService,
            ILogger<AuthenticationMiddleware> _logger)
        {
            Next = _next;
            TokenService = _tokenService;
            Logger = _logger;
        }

        public async Task InvokeAsync(HttpContext _context)
        {
            if (!TokenService.IsAuthenticated())
            {
                _context.User = new ClaimsPrincipal(new ClaimsIdentity());

                await Next(_context);

                return;
            }

            var principal = TokenService.GetPrincipal();

            _context.User = principal;

            await Next(_context);
        }
    }
}

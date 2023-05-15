using GT_Core.API.Services;
using GT_Core.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace GT_Core.API.Controllers
{
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private UserService UserService;

        public AccountController(UserService _userService)
        {
            UserService = _userService;
        }

        [HttpGet]
        [Route("/account/login/{_username}/{_password}")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string _username, string _password, CancellationToken _cancellationToken = default)
        {
            var result = await UserService.AuthenticateAsync(_username, _password);

            if (result.Succeeded)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.WriteToken(result.Entity);
                return Ok(token);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("/account/create")]
        public async Task<IActionResult> Create(ApplicationUser _user, CancellationToken _cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var result = await UserService.Create(_user, _cancellationToken);

                if (result.Succeeded)
                {
                    return Ok(result.Entity);
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("/account/{_id}/read")]
        public async Task<IActionResult> Read(string _id, CancellationToken _cancellationToken = default)
        {
            var result = await UserService.Read(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("/account/read")]
        public async Task<IActionResult> ReadAll(CancellationToken _cancellationToken = default)
        {
            var result = await UserService.ReadAll(_cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpPut]
        [Route("/account/update")]
        public async Task<IActionResult> Update(ApplicationUser _user, CancellationToken _cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var result = await UserService.Update(_user, _cancellationToken);

                if (result.Succeeded)
                {
                    return Ok(result.Entity);
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("/account/{_id}/delete")]
        public async Task<IActionResult> Delete(string _id, CancellationToken _cancellationToken = default)
        {
            var result = await UserService.Delete(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }
    }
}

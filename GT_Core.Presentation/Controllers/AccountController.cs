using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GT_Core.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using GT_Core.Presentation.Services;
using System.Security.Claims;
using GT_Core.Application.Common.Interfaces;

namespace GT_Core.Presentation.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserServiceClient UserService;
        private readonly ITokenHandler TokenService;
        private readonly ILogger<AccountController> Logger;
        public AccountController(
            UserServiceClient _identityService,
            ITokenHandler _tokenService,
            ILogger<AccountController> logger)
        {
            UserService = _identityService;
            TokenService = _tokenService;
            Logger = logger;
        }

        [AllowAnonymous]

        public async Task<IActionResult> Login()
        {
            LoginViewModel model = new LoginViewModel();

            model.ExternalLogins = new List<AuthenticationScheme>();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserService.LoginAsync(model.UserName, model.Password);

                if (result.Succeeded)
                {
                    TokenService.SetToken(result.Entity);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            TokenService.SetToken(null);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PasswordHash = model.Password
                };

                var result = await UserService.Create(user);

                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, false);
                    RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(model);
        }
    }
}
using GT_Core.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GT_Core.API.Controllers
{
    public class AccountController : Controller
    {
        private IdentityService IdentityService;

        public AccountController(IdentityService _identityService)
        {
            IdentityService = _identityService;
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}

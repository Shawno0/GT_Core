using GT_Core.Application.Common.Interfaces;
using GT_Core.Infrastructure.Identity;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GT_Core.Presentation.Services;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace GT_Core.Presentation.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> Logger;
        private readonly TicketServiceClient TicketService;
        private readonly StatusServiceClient StatusService;
        private readonly SeverityServiceClient SeverityService;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;

        public AdminController(
            ILogger<AdminController> _logger,
            TicketServiceClient _ticketService,
            StatusServiceClient _statusService,
            SeverityServiceClient _severityService,
            UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager)
        {
            Logger = _logger;
            TicketService = _ticketService;
            StatusService = _statusService;
            SeverityService = _severityService;
            UserManager = _userManager;
            RoleManager = _roleManager;
        }

        public async Task<IActionResult> Index()
        {
            AdminViewModel model = new AdminViewModel();

            var statusResult = await StatusService.ReadAll();
            var severityResult = await SeverityService.ReadAll();

            model.StatusPanel.Statuses = statusResult.Entity?.Select(s => new StatusViewModel(s)) ?? new List<StatusViewModel>();
            model.SeverityPanel.Severities = severityResult.Entity?.Select(s => new SeverityViewModel(s)) ?? new List<SeverityViewModel>();
            model.RolePanel.Roles = RoleManager.Roles.Select(s => new RoleViewModel(s))?.ToList() ?? new List<RoleViewModel>();
            model.RolePanel.Users = model.UserPanel.Users = UserManager.Users.Select(u => new UserViewModel(u))?.ToList() ?? new List<UserViewModel>();

            return View(model);
        }

        public async Task<IActionResult> StatusPanel()
        {
            StatusPanelViewModel model = new StatusPanelViewModel();

            var severityResult = await StatusService.ReadAll();

            model.Statuses = severityResult.Entity?.Select(s => new StatusViewModel(s)) ?? new List<StatusViewModel>();

            return PartialView("_AdminStatusPanelPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus(StatusViewModel _status)
        {
            Result<Status> result = await StatusService.Create(_status.Map());

            return RedirectToAction("StatusPanel");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(StatusViewModel _status)
        {
            Result<Status> result = await StatusService.Update(_status.Map());

            return RedirectToAction("StatusPanel");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStatus(StatusViewModel _status)
        {
            Result<Status> result = await StatusService.Delete(_status.Id);

            return RedirectToAction("StatusPanel");
        }

        public async Task<IActionResult> SeverityPanel()
        {
            SeverityPanelViewModel model = new SeverityPanelViewModel();

            var severityResult = await SeverityService.ReadAll();

            model.Severities = severityResult.Entity?.Select(s => new SeverityViewModel(s)) ?? new List<SeverityViewModel>();

            return PartialView("_AdminSeverityPanelPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeverity(SeverityViewModel _severity)
        {
            Result<Severity> result = await SeverityService.Create(_severity.Map());

            return RedirectToAction("SeverityPanel");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSeverity(SeverityViewModel _severity)
        {
            Result<Severity> result = await SeverityService.Update(_severity.Map());

            return RedirectToAction("SeverityPanel");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSeverity(SeverityPanelViewModel _severityPanel)
        {
            Result<Severity> result = await SeverityService.Delete(_severityPanel.DeleteSeverity.Id);

            return RedirectToAction("SeverityPanel");
        }

        public async Task<IActionResult> RolePanel()
        {
            RolePanelViewModel model = new RolePanelViewModel();

            model.Roles = RoleManager.Roles.Select(r => new RoleViewModel(r))?.ToList() ?? new List<RoleViewModel>();
            model.Users = UserManager.Users.Select(u => new UserViewModel(u))?.ToList() ?? new List<UserViewModel>();
            //model.UserRoles = RoleManager.Roles.Select(r => r.)

            return PartialView("_AdminRolePanelPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel _role)
        {
            var result = await RoleManager.CreateAsync(_role.Map());

            return RedirectToAction("RolePanel");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleViewModel _role)
        {
            var role = await RoleManager.FindByIdAsync(_role.Id);

            if (role != null)
            {
                role.Name = _role.Name;
                role.NormalizedName = _role.Name.ToUpper();

                var result = await RoleManager.UpdateAsync(role);
            }

            return RedirectToAction("RolePanel");
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel _userRole)
        {
            var role = await RoleManager.FindByIdAsync(_userRole.RoleId);

            if (role != null)
            {
                var user = await UserManager.FindByIdAsync(_userRole.UserId);

                if (user != null)
                {
                    await UserManager.AddToRoleAsync(user, role.Name);
                }
            }

            return RedirectToAction("RolePanel");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(RoleViewModel _role)
        {
            var role = await RoleManager.FindByIdAsync(_role.Id);

            if (role != null)
            {
                var result = await RoleManager.DeleteAsync(_role.Map());
            }    

            return RedirectToAction("RolePanel");
        }
    }
}
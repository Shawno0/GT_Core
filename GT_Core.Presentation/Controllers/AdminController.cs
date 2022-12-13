using GT_Core.Application.Common.Interfaces;
using GT_Core.Infrastructure.Identity;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TS_Core.Presentation.Services;

namespace GT_Core.Presentation.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> Logger;
        private readonly TicketServiceClient TicketService;
        private readonly StatusServiceClient StatusService;
        private readonly SeverityServiceClient SeverityService;
        private readonly UserManager<ApplicationUser> UserManager;

        public AdminController(
            ILogger<AdminController> _logger,
            TicketServiceClient _ticketService,
            StatusServiceClient _statusService,
            SeverityServiceClient _severityService,
            UserManager<ApplicationUser> _userManager)
        {
            Logger = _logger;
            TicketService = _ticketService;
            StatusService = _statusService;
            SeverityService = _severityService;
            UserManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            AdminViewModel model = new AdminViewModel();

            var statusResult = await StatusService.ReadAll();
            var severityResult = await SeverityService.ReadAll();

            model.StatusPanel.Statuses = statusResult.Entity?.Select(s => new StatusViewModel(s)) ?? new List<StatusViewModel>();
            model.SeverityPanel.Severities = severityResult.Entity?.Select(s => new SeverityViewModel(s)) ?? new List<SeverityViewModel>();
            model.UserPanel.Users = UserManager.Users.Select(u => new UserViewModel(u)).ToList() ?? new List<UserViewModel>();

            return View(model);
        }
    }
}
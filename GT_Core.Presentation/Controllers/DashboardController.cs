using GT_Core.Application.Common.Interfaces;
using GT_Core.Infrastructure.Identity;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GT_Core.Presentation.Services;

namespace GT_Core.Presentation.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<HomeController> Logger;
        private readonly TicketServiceClient TicketService;
        private readonly StatusServiceClient StatusService;
        private readonly SeverityServiceClient SeverityService;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly ICurrentUserService UserService;

        public DashboardController(
            ILogger<HomeController> _logger,
            TicketServiceClient _ticketService,
            StatusServiceClient _statusService,
            SeverityServiceClient _severityService,
            UserManager<ApplicationUser> _userManager,
            ICurrentUserService _userService)
        {
            Logger = _logger;
            TicketService = _ticketService;
            StatusService = _statusService;
            SeverityService = _severityService;
            UserManager = _userManager;
            UserService = _userService;
        }

        public async Task<IActionResult> Index()
        {
            DashboardViewModel model = new DashboardViewModel();
            var severitiesResult = await SeverityService.ReadAll();
            var ticketsResult = await TicketService.ReadAll();

            if (!severitiesResult.Succeeded)
            {
                Logger.LogError("Error reading Severities from Db.", severitiesResult.Errors);
            }

            if (!ticketsResult.Succeeded)
            {
                Logger.LogError("Error reading Tickets from Db.", ticketsResult.Errors);
            }

            model.Severities = severitiesResult.Entity?.Select(s => new SeverityViewModel(s)) ?? new List<SeverityViewModel>();
            model.Tickets = ticketsResult.Entity?.Select(t => new TicketViewModel(t)) ?? new List<TicketViewModel>();

            return View(model);
        }

        public async Task<IActionResult> All()
        {
            var ticketResult = await TicketService.ReadAll();

            if (!ticketResult.Succeeded)
            {
                Logger.LogError($"Error reading tickets for User {User?.Identity?.Name}.", ticketResult.Errors);
            }

            return PartialView("_DashboardPartial", ticketResult.Entity?.Select(t => new TicketViewModel(t)) ?? new List<TicketViewModel>());
        }

        public async Task<IActionResult> Ticket(int _id)
        {
            var ticketResult = await TicketService.Read(_id);

            if (ticketResult.Succeeded)
            {
                return PartialView("_ViewTicketPartial", new TicketViewModel(ticketResult.Entity));
            }

            return RedirectToAction("All");
        }

        public async Task<IActionResult> MyTickets()
        {
            var ticketResult = await TicketService.ReadByUser(UserService.UserId ?? string.Empty);

            if (!ticketResult.Succeeded)
            {
                Logger.LogError($"Error reading tickets for User {User?.Identity?.Name}.", ticketResult.Errors);
            }

            return PartialView("_DashboardPartial", ticketResult.Entity?.Select(t => new TicketViewModel(t)) ?? new List<TicketViewModel>());
        }

        public async Task<IActionResult> TicketsBySeverity(int _id)
        {
            var ticketResult = await TicketService.ReadBySeverity(_id);

            if (!ticketResult.Succeeded)
            {
                Logger.LogError($"Error reading tickets for User {User?.Identity?.Name}.", ticketResult.Errors);
            }

            return PartialView("_DashboardPartial", ticketResult.Entity?.Select(t => new TicketViewModel(t)) ?? new List<TicketViewModel>());
        }
    }
}
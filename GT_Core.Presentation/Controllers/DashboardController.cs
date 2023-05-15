using GT_Core.Application.Common.Interfaces;
using GT_Core.Infrastructure.Identity;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GT_Core.Presentation.Services;
using Microsoft.AspNetCore.Authorization;
using GT_Core.Domain.Entities;
using System.Security.Claims;

namespace GT_Core.Presentation.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger<HomeController> Logger;
        private readonly TicketServiceClient TicketService;
        private readonly EntityServiceClient<int, Status> StatusService;
        private readonly EntityServiceClient<int, Severity> SeverityService;

        public DashboardController(
            ILogger<HomeController> _logger,
            TicketServiceClient _ticketService,
            EntityServiceClient<int, Status> _statusService,
            EntityServiceClient<int, Severity> _severityService)
        {
            Logger = _logger;
            TicketService = _ticketService;
            StatusService = _statusService;
            SeverityService = _severityService;
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
            var ticketResult = await TicketService.ReadByUser(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);

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
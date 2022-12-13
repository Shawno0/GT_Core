using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Entities;
using GT_Core.Infrastructure.Identity;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TS_Core.Presentation.Services;

namespace GT_Core.Presentation.Controllers
{
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> Logger;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly TicketServiceClient TicketService;
        private readonly SeverityServiceClient SeverityService;

        public TicketController(
            ILogger<TicketController> _logger,
            UserManager<ApplicationUser> _userManager,
            TicketServiceClient _ticketService,
            SeverityServiceClient _severityService)
        {
            Logger = _logger;
            UserManager = _userManager;
            TicketService = _ticketService;
            SeverityService = _severityService;
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateTicketViewModel();

            var severitiesResult = await SeverityService.ReadAll();
            var users = UserManager.Users?.Select(u => new UserViewModel(u))?.ToList() ?? new List<UserViewModel>();

            model.Severities = severitiesResult.Entity?.Select(s => new SeverityViewModel(s)) ?? new List<SeverityViewModel>();
            model.Users = users;

            return PartialView("_CreateTicketPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketViewModel _model)
        {
            _model.Ticket.Severity = _model.Severities.FirstOrDefault(s => s.Id == _model.Severity);
            _model.Ticket.Developer = new UserViewModel(UserManager.Users.FirstOrDefault(u => u.Id == _model.Developer));
            _model.Ticket.Consultant = new UserViewModel(UserManager.Users.FirstOrDefault(u => u.Id == _model.Consultant));

            Result<Ticket> result = await TicketService.Create(_model.Ticket.Map());

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);

                    return PartialView("_CreateTicketPartial", _model);
                }
            }

            return RedirectToAction("Ticket", "Dashboard", result.Entity.Id);
        }

        public async Task<TicketViewModel> Read(int _id)
        {
            Result<Ticket> result = await TicketService.Read(_id);

            if (result.Succeeded)
            {
                return new TicketViewModel(result.Entity, UserManager);
            }

            return new TicketViewModel();
        }

        public async Task<IEnumerable<TicketViewModel>> ReadRange(int _startId, int _endId)
        {
            Result<IEnumerable<Ticket>> result = await TicketService.ReadRange(_startId, _endId);

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new TicketViewModel(e, UserManager))?.ToList() ?? new List<TicketViewModel>();
            }

            return new List<TicketViewModel>();
        }

        public async Task<IEnumerable<TicketViewModel>> ReadAll()
        {
            Result<IEnumerable<Ticket>> result = await TicketService.ReadAll();

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new TicketViewModel(e, UserManager))?.ToList() ?? new List<TicketViewModel>();
            }

            return new List<TicketViewModel>();
        }

        public async Task<IEnumerable<TicketViewModel>> ReadByStatus(int _statusId)
        {
            Result<IEnumerable<Ticket>> result = await TicketService.ReadByStatus(_statusId);

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new TicketViewModel(e, UserManager))?.ToList() ?? new List<TicketViewModel>();
            }

            return new List<TicketViewModel>();
        }

        public async Task<IEnumerable<TicketViewModel>> ReadBySeverity(int _severityId)
        {
            Result<IEnumerable<Ticket>> result = await TicketService.ReadByStatus(_severityId);

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new TicketViewModel(e, UserManager))?.ToList() ?? new List<TicketViewModel>();
            }

            return new List<TicketViewModel>();
        }

        public async Task<IEnumerable<TicketViewModel>> ReadByUser(string _userId)
        {
            Result<IEnumerable<Ticket>> result = await TicketService.ReadByUser(_userId);

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new TicketViewModel(e, UserManager))?.ToList() ?? new List<TicketViewModel>();
            }

            return new List<TicketViewModel>();
        }

        public async Task<TicketViewModel> Update(TicketViewModel _ticket)
        {
            Result<Ticket> result = await TicketService.Update(_ticket.Map());

            if (result.Succeeded)
            {
                return new TicketViewModel(result.Entity, UserManager);
            }

            return _ticket;
        }

        public async Task<TicketViewModel> Delete(TicketViewModel _ticket)
        {
            Result<Ticket> result = await TicketService.Delete(_ticket.Id);

            if (result.Succeeded)
            {
                return new TicketViewModel(result.Entity, UserManager);
            }

            return _ticket;
        }
    }
}
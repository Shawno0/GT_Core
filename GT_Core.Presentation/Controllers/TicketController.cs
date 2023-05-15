using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Domain.Entities;
using GT_Core.Infrastructure.Identity;
using GT_Core.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GT_Core.Presentation.Services;
using Microsoft.AspNetCore.Authorization;

namespace GT_Core.Presentation.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> Logger;
        private readonly UserServiceClient UserService;
        private readonly TicketServiceClient TicketService;
        private readonly EntityServiceClient<int, Severity> SeverityService;
        private readonly EntityServiceClient<int, Status> StatusService;

        public TicketController(
            ILogger<TicketController> _logger,
            UserServiceClient _userService,
            TicketServiceClient _ticketService,
            EntityServiceClient<int, Severity> _severityService,
            EntityServiceClient<int, Status> _statusService)
        {
            Logger = _logger;
            UserService = _userService;
            TicketService = _ticketService;
            SeverityService = _severityService;
            StatusService = _statusService;
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateTicketViewModel();

            var severitiesResult = await SeverityService.ReadAll();
            var usersResult = await UserService.ReadAll();

            model.Severities = severitiesResult.Entity?.Select(s => new SeverityViewModel(s)) ?? new List<SeverityViewModel>();
            model.Users = usersResult.Entity?.Select(u => new UserViewModel(u))?.ToList() ?? new List<UserViewModel>();

            return PartialView("_CreateTicketPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketViewModel _model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateTicketPartial", _model);
            }

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
                return new TicketViewModel(result.Entity);
            }

            return new TicketViewModel();
        }

        public async Task<IEnumerable<TicketViewModel>> ReadRange(int _startId, int _endId)
        {
            Result<IEnumerable<Ticket>> result = await TicketService.ReadRange(_startId, _endId);

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new TicketViewModel(e))?.ToList() ?? new List<TicketViewModel>();
            }

            return new List<TicketViewModel>();
        }

        public async Task<IEnumerable<TicketViewModel>> ReadAll()
        {
            Result<IEnumerable<Ticket>> result = await TicketService.ReadAll();

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new TicketViewModel(e))?.ToList() ?? new List<TicketViewModel>();
            }

            return new List<TicketViewModel>();
        }

        public async Task<IEnumerable<TicketViewModel>> ReadByStatus(int _statusId)
        {
            Result<IEnumerable<Ticket>> result = await TicketService.ReadByStatus(_statusId);

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new TicketViewModel(e))?.ToList() ?? new List<TicketViewModel>();
            }

            return new List<TicketViewModel>();
        }

        public async Task<IEnumerable<TicketViewModel>> ReadBySeverity(int _severityId)
        {
            Result<IEnumerable<Ticket>> result = await TicketService.ReadByStatus(_severityId);

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new TicketViewModel(e))?.ToList() ?? new List<TicketViewModel>();
            }

            return new List<TicketViewModel>();
        }

        public async Task<IEnumerable<TicketViewModel>> ReadByUser(string _userId)
        {
            Result<IEnumerable<Ticket>> result = await TicketService.ReadByUser(_userId);

            if (result.Succeeded)
            {
                return result.Entity?.Select(e => new TicketViewModel(e))?.ToList() ?? new List<TicketViewModel>();
            }

            return new List<TicketViewModel>();
        }

        public async Task<TicketViewModel> Update(TicketViewModel _ticket)
        {
            Result<Ticket> result = await TicketService.Update(_ticket.Map());

            if (result.Succeeded)
            {
                return new TicketViewModel(result.Entity);
            }

            return _ticket;
        }

        public async Task<TicketViewModel> Delete(TicketViewModel _ticket)
        {
            Result<Ticket> result = await TicketService.Delete(_ticket.Id);

            if (result.Succeeded)
            {
                return new TicketViewModel(result.Entity);
            }

            return _ticket;
        }
    }
}
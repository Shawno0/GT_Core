using GT_Core.API.Services;
using GT_Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GT_Core.API.Controllers
{
    [ApiController]
    [Authorize]
    public class TicketController : Controller
    {
        private readonly TicketService TicketService;
        private readonly SeverityService SeverityService;
        private readonly StatusService StatusService;
        private readonly CommentService CommentService;

        public TicketController(
            TicketService _ticketService,
            SeverityService _severityService,
            StatusService _statusService,
            CommentService _commentService)
        {
            TicketService = _ticketService;
            SeverityService = _severityService;
            StatusService = _statusService;
            CommentService = _commentService;
        }

        [HttpPost]
        [Route("/ticket/create")]
        public async Task<IActionResult> Create(Ticket _ticket, CancellationToken _cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var result = await TicketService.Create(_ticket, _cancellationToken);

                if (result.Succeeded)
                {
                    return Ok(result.Entity);
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("/ticket/{_id}/read")]
        public async Task<IActionResult> Read(int _id, CancellationToken _cancellationToken = default)
        {
            var result = await TicketService.Read(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("/ticket/{_firstId}/{_lastId}/read")]
        public async Task<IActionResult> ReadRange(int _firstId, int _lastId, CancellationToken _cancellationToken = default)
        {
            var result = await TicketService.ReadRange(_firstId, _lastId, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("/ticket/read")]
        public async Task<IActionResult> ReadAll(CancellationToken _cancellationToken = default)
        {
            var result = await TicketService.ReadAll(_cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("/ticket/severity/{_id}/read")]
        public async Task<IActionResult> ReadBySeverity(int _id, CancellationToken _cancellationToken = default)
        {
            var result = await TicketService.ReadBySeverity(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("/ticket/status/{_id}/read")]
        public async Task<IActionResult> ReadByStatus(int _id, CancellationToken _cancellationToken = default)
        {
            var result = await TicketService.ReadByStatus(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("/ticket/user/{_id}/read")]
        public async Task<IActionResult> ReadByUser(string _id, CancellationToken _cancellationToken = default)
        {
            var result = await TicketService.ReadByUser(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpPut]
        [Route("/ticket/update")]
        public async Task<IActionResult> Update(Ticket _ticket, CancellationToken _cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var result = await TicketService.Update(_ticket, _cancellationToken);

                if (result.Succeeded)
                {
                    return Ok(result.Entity);
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("/ticket/{_id}/delete")]
        public async Task<IActionResult> Delete(int _id, CancellationToken _cancellationToken = default)
        {
            var result = await TicketService.Delete(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }
    }
}
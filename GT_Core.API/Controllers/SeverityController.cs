using GT_Core.API.Services;
using GT_Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GT_Core.API.Controllers
{
    [ApiController]
    [Authorize]
    public class SeverityController : Controller
    {
        private readonly SeverityService SeverityService;

        public SeverityController(SeverityService _severityService)
        {
            SeverityService = _severityService;
        }

        [HttpPost]
        [Route("/severity/create")]
        public async Task<IActionResult> Create(Severity _severity, CancellationToken _cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var result = await SeverityService.Create(_severity, _cancellationToken);

                if (result.Succeeded)
                {
                    return Ok(result.Entity);
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("/severity/{_id}/read")]
        public async Task<IActionResult> Read(int _id, CancellationToken _cancellationToken = default)
        {
            var result = await SeverityService.Read(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("/severity/{_firstId}/{_lastId}/read")]
        public async Task<IActionResult> ReadRange(int _firstId, int _lastId, CancellationToken _cancellationToken = default)
        {
            var result = await SeverityService.ReadRange(_firstId, _lastId, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("/severity/read")]
        public async Task<IActionResult> ReadAll(CancellationToken _cancellationToken = default)
        {
            var result = await SeverityService.ReadAll(_cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpPut]
        [Route("/severity/update")]
        public async Task<IActionResult> Update(Severity _severity, CancellationToken _cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var result = await SeverityService.Update(_severity, _cancellationToken);

                if (result.Succeeded)
                {
                    return Ok(result.Entity);
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("/severity/{_id}/delete")]
        public async Task<IActionResult> Delete(int _id, CancellationToken _cancellationToken = default)
        {
            var result = await SeverityService.Delete(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }
    }
}
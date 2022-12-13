using GT_Core.API.Services;
using GT_Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GT_Core.API.Controllers
{
    [ApiController]
    public class StatusController : Controller
    {
        private readonly StatusService StatusService;

        public StatusController(StatusService _statusService)
        {
            StatusService = _statusService;
        }

        [HttpPost]
        [Route("/status/create")]
        public async Task<IActionResult> Create(Status _status, CancellationToken _cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var result = await StatusService.Create(_status, _cancellationToken);

                if (result.Succeeded)
                {
                    return Ok(result.Entity);
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("/status/{_id}/read")]
        public async Task<IActionResult> Read(int _id, CancellationToken _cancellationToken = default)
        {
            var result = await StatusService.Read(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("/status/{_firstId}/{_lastId}/read")]
        public async Task<IActionResult> ReadRange(int _firstId, int _lastId, CancellationToken _cancellationToken = default)
        {
            var result = await StatusService.ReadRange(_firstId, _lastId, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("/status/read")]
        public async Task<IActionResult> ReadAll(CancellationToken _cancellationToken = default)
        {
            var result = await StatusService.ReadAll(_cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpPut]
        [Route("/status/update")]
        public async Task<IActionResult> Update(Status _status, CancellationToken _cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var result = await StatusService.Update(_status, _cancellationToken);

                if (result.Succeeded)
                {
                    return Ok(result.Entity);
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("/status/{_id}/delete")]
        public async Task<IActionResult> Delete(int _id, CancellationToken _cancellationToken = default)
        {
            var result = await StatusService.Delete(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }
    }
}
using GT_Core.API.Services;
using GT_Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GT_Core.API.Controllers
{
    [ApiController]
    [Authorize]
    public class CommentController : Controller
    {
        private readonly CommentService CommentService;

        public CommentController(CommentService _commentService)
        {
            CommentService = _commentService;
        }

        [HttpPost]
        [Route("/comment/create")]
        public async Task<IActionResult> Create(Comment _comment, CancellationToken _cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var result = await CommentService.Create(_comment, _cancellationToken);

                if (result.Succeeded)
                {
                    return Ok(result.Entity);
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("/comment/{_id}/read")]
        public async Task<IActionResult> Read(int _id, CancellationToken _cancellationToken = default)
        {
            var result = await CommentService.Read(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("/comment/{_firstId}/{_lastId}/read")]
        public async Task<IActionResult> ReadRange(int _firstId, int _lastId, CancellationToken _cancellationToken = default)
        {
            var result = await CommentService.ReadRange(_firstId, _lastId, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("/comment/read")]
        public async Task<IActionResult> ReadAll(CancellationToken _cancellationToken = default)
        {
            var result = await CommentService.ReadAll(_cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }

        [HttpPut]
        [Route("/comment/update")]
        public async Task<IActionResult> Update(Comment _comment, CancellationToken _cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var result = await CommentService.Update(_comment, _cancellationToken);

                if (result.Succeeded)
                {
                    return Ok(result.Entity);
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("/comment/{_id}/delete")]
        public async Task<IActionResult> Delete(int _id, CancellationToken _cancellationToken = default)
        {
            var result = await CommentService.Delete(_id, _cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Errors);
        }
    }
}
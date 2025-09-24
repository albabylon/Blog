using Blog.Application.Contracts.Interfaces;
using Blog.DTOs.Comment;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;


        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _commentService.GetCommentAsync(id);
            return Json(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> All()
        {
            var result = await _commentService.GetAllCommentsAsync();
            return Json(result);
        }

        [HttpPost]
        [Route("[action]/{articleId}")]
        public async Task<IActionResult> Create(CreateCommentDTO dto, int articleId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return BadRequest("Пользовательн не найден");

            await _commentService.CreateCommentAsync(dto, articleId, userId);
            return Ok();
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(EditCommentDTO dto, int id)
        {
            if (id != dto.Id)
                return BadRequest("Несоответствие ID комментария");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _commentService.EditCommentAsync(dto, userId);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentService.DeleteCommentAsync(id);
            return Ok();
        }
    }
}

using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Domain.Identity;
using Blog.DTOs.Comment;
using Blog.Web.ViewModels.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    [Route("comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var dto = await _commentService.GetAllCommentsAsync();
            return Json(dto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _commentService.GetCommentAsync(id);
            return Json(result);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> All()
        {
            var result = await _commentService.GetAllCommentsAsync();
            return Json(result);
        }



        [HttpPost]
        [Route("create/{articleId}")]
        public async Task<IActionResult> Create(CommentViewModel model, int articleId)
        {
            var dto = _mapper.Map<CreateCommentDTO>(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return BadRequest("Пользователь не найден");

            await _commentService.CreateCommentAsync(dto, articleId, userId);
            return RedirectToAction("Index", "Article", new { id = articleId });
        }

        [HttpPut]
        [Route("edit/{id}")]
        [Authorize(Roles = $"{SystemRoles.User}, {SystemRoles.Moderator}, {SystemRoles.Admin}")]
        public async Task<IActionResult> Edit(CommentViewModel model, int id)
        {
            try
            {
                var dto = _mapper.Map<EditCommentDTO>(model);

                if (id != dto.Id)
                    return BadRequest("Несоответствие ID комментария");

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _commentService.EditCommentAsync(dto, userId);
                return Json(dto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = $"{SystemRoles.User}, {SystemRoles.Moderator}, {SystemRoles.Admin}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _commentService.DeleteCommentAsync(id, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }
    }
}

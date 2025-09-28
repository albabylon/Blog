using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.Domain.Identity;
using Blog.DTOs.Article;
using Blog.Web.ViewModels.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ArticleController(IArticleService articleService, IUserService userService, IMapper mapper)
        {
            _articleService = articleService;
            _userService = userService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var dto = await _articleService.GetAllArticlesAsync();
            return View(dto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _articleService.GetArticleAsync(id);
            return Json(result);
        }

        [HttpGet]
        [Route("all/{authorId}")]
        public async Task<IActionResult> AllByAuthor(string authorId)
        {
            var result = await _articleService.GetAllArticlesByAuthorAsync(authorId);
            return Json(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> All([FromQuery(Name = "name")] string? tagName)
        {
            if (tagName is null)
            {
                var result = await _articleService.GetAllArticlesAsync();
                return Json(result);
            }
            else
            {
                var result = await _articleService.GetAllArticlesByTagAsync(tagName);
                return Json(result);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(ArticleViewModel model)
        {
            var dto = _mapper.Map<CreateArticleDTO>(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _articleService.CreateArticleAsync(dto, userId);

            return Json(dto);
        }

        [HttpPut]
        [Route("[action]/{id}")]
        [Authorize(Roles = $"{SystemRoles.User}, {SystemRoles.Moderator}, {SystemRoles.Admin}")]
        public async Task<IActionResult> Edit(ArticleEditViewModel model, int id)
        {
            var dto = _mapper.Map<EditArticleDTO>(model);
            
            if (id != dto.Id)
                return BadRequest("Несоответствие ID статьи");

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _articleService.EditArticleAsync(dto, userId);
                return Ok(new { Message = "Статья успешно обновлена" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        [Authorize(Roles = $"{SystemRoles.User}, {SystemRoles.Moderator}, {SystemRoles.Admin}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _articleService.DeleteArticleAsync(id, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }
    }
}

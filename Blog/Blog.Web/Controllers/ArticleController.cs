using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.Application.Services;
using Blog.DTOs.Article;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;

        public ArticleController(IArticleService articleService, IUserService userService)
        {
            _articleService = articleService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _articleService.CreateArticleAsync(dto, userId);

            return Json(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditArticleDTO dto, int id)
        {
            if (id != dto.Id)
                return BadRequest("Несоответствие ID статьи");

            try
            {
                var userId = User.FindFirst("id").Value;
                await _articleService.EditArticleAsync(dto, userId);
                return Ok(new { Message = "Статья успешно обновлена" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch(UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _articleService.DeleteArticleAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> AllArticle()
        {
            await _articleService.GetAllArticlesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> AllArticle(string guid)
        {
            await _articleService.GetAllArticlesByAuthorAsync(guid);
            return Ok();
        }
    }
}

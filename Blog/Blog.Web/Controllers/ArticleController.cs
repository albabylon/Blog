using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.DTOs.Article;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }


        public async Task<IActionResult> Create([FromBody] CreateArticleDTO dto)
        {
            var userId = User.FindFirst("id").Value;
            await _articleService.CreateArticleAsync(dto, userId);
            
            return Ok();
        }

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

        public async Task<IActionResult> Delete(int id)
        {
            await _articleService.DeleteArticleAsync(id);
            return Ok();
        }

        public async Task<IActionResult> AllArticle()
        {
            await _articleService.GetAllArticlesAsync();
            return Ok();
        }

        public async Task<IActionResult> AllArticle(string guid)
        {
            await _articleService.GetAllArticlesByAuthorAsync(guid);
            return Ok();
        }
    }
}

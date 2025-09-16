using Blog.Application.Contracts.Interfaces;
using Blog.DTOs.Article;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleDTO dto)
        {
            if(int.TryParse(User.FindFirst("id").Value, out int userId))
                await _articleService.CreateArticleAsync(dto, userId);
            
            return Ok();
        }
    }
}

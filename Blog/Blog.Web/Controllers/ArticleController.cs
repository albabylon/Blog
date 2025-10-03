using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.Domain.Identity;
using Blog.DTOs.Article;
using Blog.DTOs.Tag;
using Blog.Web.ViewModels.Account;
using Blog.Web.ViewModels.Article;
using Blog.Web.ViewModels.Comment;
using Blog.Web.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    [Route("article")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ArticleController(IArticleService articleService, ICommentService commentService, IUserService userService, IMapper mapper)
        {
            _articleService = articleService;
            _commentService = commentService;
            _userService = userService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var articleDto = await _articleService.GetArticleAsync(id);
            var articleModel = _mapper.Map<ArticleViewModel>(articleDto);
            
            var commentDto = await _commentService.GetAllCommentsByArticleAsync(articleDto.Id);
            var commentModel = _mapper.Map<IEnumerable<CommentViewModel>>(commentDto);

            var mainModel = new ArticleMainViewModel()
            {
                Article = articleModel,
                Comments = commentModel
            };

            return View("/Views/Article/Article.cshtml", mainModel);
        }

        [HttpGet]
        [Route("all/{authorId}")]
        public async Task<IActionResult> AllByAuthor(string authorId)
        {
            var result = await _articleService.GetAllArticlesByAuthorAsync(authorId);
            return Json(result);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> All([FromQuery(Name = "name")] string? tagName)
        {
            if (tagName is null)
            {
                var dto = await _articleService.GetAllArticlesAsync();
                var model = _mapper.Map<IEnumerable<ArticleViewModel>>(dto);
                return View("ArticleList", model);
            }
            else
            {
                var dto = await _articleService.GetAllArticlesByTagAsync(tagName);
                var model = _mapper.Map<IEnumerable<ArticleViewModel>>(dto);
                return View("ArticleList", model);
            }
        }



        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            var model = new ArticleViewModel() { Tags = new List<TagViewModel>(), TagNames = new List<string>() };
            return View("/Views/Article/ArticleCreate.cshtml", model);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(ArticleViewModel model)
        {
            var dto = _mapper.Map<CreateArticleDTO>(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _articleService.CreateArticleAsync(dto, userId);
            
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _articleService.GetArticleAsync(id);
            var model = _mapper.Map<ArticleViewModel>(dto);

            foreach (var tag in model.Tags)
            {
                tag.IsCheked = true;
            }

            return View("/Views/Article/ArticleCreate.cshtml", model);
        }

        [HttpPost]
        [Route("edit/{id}")]
        [Authorize(Roles = $"{SystemRoles.User}, {SystemRoles.Moderator}, {SystemRoles.Admin}")]
        public async Task<IActionResult> Edit(ArticleViewModel model, int id)
        {
            var dto = _mapper.Map<EditArticleDTO>(model);

            if (id != dto.Id)
                return BadRequest("Несоответствие ID статьи");

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _articleService.EditArticleAsync(dto, userId);
                return View(new { Message = "Статья успешно обновлена" });
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



        [HttpPost]
        [Route("delete/{id}")]
        [Authorize(Roles = $"{SystemRoles.User}, {SystemRoles.Moderator}, {SystemRoles.Admin}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _articleService.DeleteArticleAsync(id, userId);

                return RedirectToAction("Index", "Profile");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }
    }
}

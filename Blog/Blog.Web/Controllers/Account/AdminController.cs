using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.Domain.Identity;
using Blog.DTOs.Article;
using Blog.DTOs.User;
using Blog.Web.ViewModels.Account;
using Blog.Web.ViewModels.Article;
using Blog.Web.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers.Account
{
    [Authorize(Roles = $"{SystemRoles.Moderator}, {SystemRoles.Admin}")]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IArticleService _articleService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public AdminController(IUserService userService, IArticleService articleService, ITagService tagService, IMapper mapper)
        {
            _userService = userService;
            _articleService = articleService;
            _tagService = tagService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {   
            var users = await _userService.GetAllUsersAsync();
            var modelUser = _mapper.Map<IEnumerable<ProfileViewModel>>(users);

            var articles = await _articleService.GetAllArticlesAsync();
            var modelArticle = _mapper.Map< IEnumerable<ArticleViewModel>>(articles);

            var tags = await _tagService.GetAllTagsAsync();
            var modelTags = _mapper.Map<IEnumerable<TagViewModel>>(tags);

            var model = new AdminViewModel()
            {
                Users = modelUser,
                Articles = modelArticle,
                Tags = modelTags,
            };

            return View("/Views/Account/AdminPanel.cshtml", model);
        }


        //Users
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var userId = id;
            
            var user = await _userService.GetUserByIdAsync(userId);
            var model = _mapper.Map<ProfileViewModel>(user);

            var articles = await _articleService.GetAllArticlesByAuthorAsync(userId);
            model.Articles = _mapper.Map<IEnumerable<ArticleViewModel>>(articles);

            return View("/Views/Account/User.cshtml", model);
        }

        [HttpGet]
        [Route("edit/user/{id}")]
        public async Task<IActionResult> EditUser(string id)
        {
            var userId = id;
            
            var dto = await _userService.GetUserByIdAsync(userId);
            var model = _mapper.Map<ProfileEditViewModel>(dto);

            return View("/Views/Account/AdminProfileEdit.cshtml", model);
        }

        [HttpPost]
        [Route("edit/user/{id}")]
        public async Task<IActionResult> EditUser(ProfileEditViewModel model, string id)
        {
            try
            {
                var userId = id;
                var dto = _mapper.Map<EditUserDTO>(model);

                await _userService.EditUserAsync(dto, userId);
                return RedirectToAction("Index");
            }
            catch (UserProblemException ex)
            {
                ModelState.AddModelError("", "Пользователь не найден");
                return Content($"{ex.Message}");
            }
        }

        [HttpPost]
        [Route("delete/user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userId = id;

            var result = await _userService.DeleteUserAsync(userId);
            if (result)
                return RedirectToAction("Index");

            return Content($"Удалить пользователя {userId} не получилось");
        }

        //Roles

        //Articles
        [HttpGet]
        [Route("edit/article/{id}")]
        public async Task<IActionResult> EditArticle(int id)
        {
            var dto = await _articleService.GetArticleAsync(id);
            var model = _mapper.Map<ArticleViewModel>(dto);

            return View("/Views/Account/AdminArticleEdit.cshtml", model);
        }

        [HttpPost]
        [Route("edit/article/{id}")]
        public async Task<IActionResult> EditArticle(ArticleViewModel model, int id)
        {
            var dto = _mapper.Map<EditArticleDTO>(model);

            if (id != dto.Id)
                return BadRequest("Несоответствие ID статьи");

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _articleService.EditArticleAsync(dto, userId);
                return RedirectToAction("Index");
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
        public async Task<IActionResult> DeleteArticle(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _articleService.DeleteArticleAsync(id, userId);

                return RedirectToAction("Index");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        //Tags

        [HttpPost]
        [Route("updaterole")]
        public async Task<IActionResult> UpdateRole(string userId, string role)
        {
            var result = await _userService.UpdateUserRoleAsync(userId, role);

            if (result)
                return Content($"Обновлена роль у {userId} до {role}");
            return Content($"Обновить роль у {userId} до {role} не получилось");
        }
    }
}

using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Domain.Identity;
using Blog.Web.ViewModels.Account;
using Blog.Web.ViewModels.Article;
using Blog.Web.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers.Account
{
    [Authorize(Roles = SystemRoles.Admin)]
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


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var allUsers = await _userService.GetUserByIdAsync(userId);
            return Json(allUsers);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result)
                return Content($"Удален пользователь: {id}");
            return Content($"Удалить пользователя {id} не получилось");
        }

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

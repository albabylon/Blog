using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.DTOs.User;
using Blog.Web.ViewModels.Account;
using Blog.Web.ViewModels.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers.Account
{
    [Route("profile")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IArticleService _articleService;

        public ProfileController(IUserService userService, IMapper mapper, IArticleService articleService)
        {
            _userService = userService;
            _mapper = mapper;
            _articleService = articleService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) 
                ?? throw new Exception("Пользователь не найден");

            var dto = await _userService.GetUserByIdAsync(userId);
            var model = _mapper.Map<ProfileViewModel>(dto);

            var articles = await _articleService.GetAllArticlesByAuthorAsync(userId);
            model.Articles = _mapper.Map<IEnumerable<ArticleViewModel>>(articles);

            return View("~/Views/Account/Profile.cshtml", model);
        }



        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = await _userService.GetUserByIdAsync(userId);

            var model = _mapper.Map<ProfileEditViewModel>(dto);

            return View("/Views/Account/ProfileEdit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(ProfileEditViewModel model)
        {
            try
            {
                var dto = _mapper.Map<EditUserDTO>(model);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _userService.EditUserAsync(dto, userId);
                return RedirectToAction("Index");
            }
            catch (UserProblemException ex)
            {
                ModelState.AddModelError("", "Пользователь не найден");
                return Content($"{ex.Message}");
            }
        }
    }
}

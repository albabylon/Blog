using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.DTOs.User;
using Blog.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers.Account
{
    [Route("[action]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Bio ??= string.Empty;
                model.ProfilePictureUrl ??= string.Empty;

                var createUser = _mapper.Map<CreateUserDTO>(model);
                var result = await _userService.CreateUserAsync(createUser);

                if (result)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError("", "Ошибка при регистрации");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //от CSRF-атак
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = _mapper.Map<LoginUserDTO>(model);
                    var isLogged = await _userService.LoginUserAsync(dto);

                    if (isLogged)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            ModelState.Clear();
                            return RedirectToAction("All", "Article");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    }
                }
                catch (NotFoundException ex)
                {
                    ModelState.AddModelError("", "Не найден пользователь");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutUserAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

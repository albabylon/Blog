using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.DTOs.User;
using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Blog.Web.Controllers.Account
{
    [Route("[action]")]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProfileController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //от CSRF-атак
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = _mapper.Map<LoginUserDTO>(viewModel);
                    var result = await _userService.LoginUserAsync(dto);

                    if (result)
                    {
                        if (!string.IsNullOrEmpty(viewModel.ReturnUrl) && Url.IsLocalUrl(viewModel.ReturnUrl))
                        {
                            return Redirect(viewModel.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("MyPage", "AccountManager");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                        return Content($"Неправильный логин и (или) пароль");
                    }
                }
                catch (NotFoundException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return Content($"{ex.Message}");
                }
            }
            return View("Views/Home/Index.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutUserAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPut]
        [Authorize] //пройдена авторизация и аутентификация
        public async Task<IActionResult> Edit(EditUserDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _userService.EditUserAsync(dto, userId);
                return Json(dto);                
                //return View("Edit", editUser);
            }
            catch (UserProblemException ex)
            {
                ModelState.AddModelError("", "Пользователь не найден");
                return Content($"{ex.Message}");
                //return View("Views/Home/Index.cshtml");
            }
        }
    }
}

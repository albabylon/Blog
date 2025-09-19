using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers.Account
{
    [Route("[action]")]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken] //от CSRF-атак
        public async Task<IActionResult> Login(LoginUserDTO dto, string? returnUrl) //returnUrl запихать во viewmodel
        {
            //if (ModelState.IsValid)
            //{
                try
                {
                    var result = await _userService.LoginUserAsync(dto);

                    if (result)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Content($"{returnUrl}");
                            //return Redirect(returnUrl);
                        }
                        else
                        {
                            return Content($"url пустая");
                            //return RedirectToAction("MyPage", "AccountManager");
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
            //}
            //return Json(dto);
            //return View("Views/Home/Index.cshtml", new MainViewModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutUserAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        //[Authorize] //пройдена авторизация и аутентификация
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

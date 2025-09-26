using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers.Account
{
    [Route("[action]")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Просмотр профиля текущего пользователя
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            // Редактирование профиля
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditUserDTO dto)
        {
            // Сохранение изменений профиля
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

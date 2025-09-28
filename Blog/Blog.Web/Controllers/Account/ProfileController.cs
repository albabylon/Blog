using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.DTOs.User;
using Blog.Web.ViewModels.Account;
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
        private readonly IMapper _mapper;

        public ProfileController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = await _userService.GetUserByIdAsync(userId);
            return View(dto);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            // Редактирование профиля
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(ProfileEditViewModel model)
        {
            // Сохранение изменений профиля
            try
            {
                var dto = _mapper.Map<EditUserDTO>(model);

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

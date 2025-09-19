using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers.Account
{
    [Route("[action]")]
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDTO createUser)
        {
            var result = await _userService.CreateUserAsync(createUser);

            if (result)
                return Json(createUser);
            else
                return Content("Не получилось");
        }
    }
}

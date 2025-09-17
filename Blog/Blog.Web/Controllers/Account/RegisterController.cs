using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers.Account
{
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register()
        {
            try
            {
                _userService.CreateUserAsync(new DTOs.User.CreateUserDTO());
                return View();
            }
            catch (UserNotCreatedException)
            {
                return Ok("Не получилось");
            };
        }
    }
}

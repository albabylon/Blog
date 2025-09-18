using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.DTOs.User;
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
        [Route("[action]")]
        public async Task<IActionResult> Register(CreateUserDTO createUser)
        {
            try
            {
                await _userService.CreateUserAsync(createUser);
                return Json(createUser);
            }
            catch (UserProblemException)
            {
                return Ok("Не получилось");
            };
        }
    }
}

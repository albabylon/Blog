using Blog.Application.Contracts.Interfaces;
using Blog.Domain.Identity;
using Blog.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Web.Controllers.Account
{
    //[Authorize(Roles = SystemRoles.Admin)]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("[controller]")]
        public async Task<IActionResult> Index()
        {
            var allUsers = await _userService.GetAllUsersAsync();
            return Json(allUsers);
            //return View();
        }

        [HttpPost]
        //[Route("{id}")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Delete(string id)
        {
            //await _userService.GetUserAsync();
            await _userService.DeleteUserAsync(id);
            return Content($"Удален пользователь: {id}");
        }
    }
}

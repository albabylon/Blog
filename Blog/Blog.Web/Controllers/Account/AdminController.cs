using Blog.Application.Contracts.Interfaces;
using Blog.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers.Account
{
    [Authorize(Roles = SystemRoles.Admin)]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {   
            var allUsers = await _userService.GetAllUsersAsync();
            return Json(allUsers);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var allUsers = await _userService.GetUserByIdAsync(userId);
            return Json(allUsers);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result)
                return Content($"Удален пользователь: {id}");
            return Content($"Удалить пользователя {id} не получилось");
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateRole(string userId, string role)
        {
            var result = await _userService.UpdateUserRoleAsync(userId, role);

            if (result)
                return Content($"Обновлена роль у {userId} до {role}");
            return Content($"Обновить роль у {userId} до {role} не получилось");
        }
    }
}

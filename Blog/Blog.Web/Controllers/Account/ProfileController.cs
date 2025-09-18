using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers.Account
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

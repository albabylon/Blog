using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class TagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Blog.Application.Contracts.Interfaces;
using Blog.DTOs.Tag;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var result = await _tagService.GetAllTagsAsync();
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTagDTO dto)
        {
            await _tagService.CreateArticleAsync(dto);
            return Json(dto);
        }
    }
}

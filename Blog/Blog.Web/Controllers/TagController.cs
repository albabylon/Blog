using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.DTOs.Tag;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("[controller]")]
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
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _tagService.GetTagAsync(id);
            return Json(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> All()
        {
            var result = await _tagService.GetAllTagsAsync();
            return Json(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(CreateTagDTO dto)
        {
            await _tagService.CreateTagAsync(dto);
            return Json(dto);
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(EditTagDTO dto, int id)
        {
            if (id != dto.Id)
                return BadRequest("Несоответствие ID тега");

            try
            {
                await _tagService.EditTagAsync(dto);
                return Json(dto);
            }
            catch (NotFoundException)
            {
                return NotFound(dto);
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tagService.DeleteTagAsync(id);
            return Ok(id);
        }
    }
}

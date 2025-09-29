using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.Domain.Identity;
using Blog.DTOs.Tag;
using Blog.Web.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("[controller]")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var dto = await _tagService.GetAllTagsAsync();
            return Json(dto);
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
        public async Task<IActionResult> Create(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<CreateTagDTO>(model);
                await _tagService.CreateTagAsync(dto);
                return Json(dto);
            }
            return Content("Ошибка");
        }

        [HttpPut]
        [Route("[action]/{id}")]
        [Authorize(Roles = $"{SystemRoles.User}, {SystemRoles.Moderator}, {SystemRoles.Admin}")]
        public async Task<IActionResult> Edit(TagEditViewModel model, int id)
        {
            var dto = _mapper.Map<EditTagDTO>(model);
            
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
        [Authorize(Roles = $"{SystemRoles.User}, {SystemRoles.Moderator}, {SystemRoles.Admin}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tagService.DeleteTagAsync(id);
            return Ok(id);
        }
    }
}

using AutoMapper;
using Blog.Domain.Entities;
using Blog.Infrastructure.Contracts.Interfaces;
using Blog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IMapper mapper, IUnitOfWork unitOfWork) 
        { 
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            //return View(); 
            var repo = _unitOfWork.GetRepository<Tag>() as TagsRepository;

            return StatusCode(200);
        }
    }
}

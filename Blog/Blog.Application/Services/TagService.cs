using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Domain.Entities;
using Blog.DTOs.Article;
using Blog.DTOs.Tag;
using Blog.Infrastructure.Contracts.Interfaces;
using Blog.Infrastructure.Repositories;

namespace Blog.Application.Services
{
    public class TagService : ITagService
    {
        private readonly TagsRepository _tagRepos;
        private readonly IMapper _mapper;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tagRepos = unitOfWork.GetRepository<Tag>() as TagsRepository 
                ?? throw new Exception();
            _mapper = mapper;
        }


        public async Task<IEnumerable<TagDTO>> GetAllTagsAsync()
        {
            var result = await _tagRepos.GetAll();
            return _mapper.Map<IEnumerable<TagDTO>>(result);
        }

        public async Task<IEnumerable<TagDTO>> GetAllTagsByArticleAsync(int articleId)
        {
            var result = await _tagRepos.GetAllByArticleId(articleId);
            return _mapper.Map<IEnumerable<TagDTO>>(result);
        }

        public async Task CreateArticleAsync(CreateTagDTO dto)
        {
            var tag = _mapper.Map<Tag>(dto);
            await _tagRepos.Create(tag);
        }
    }
}

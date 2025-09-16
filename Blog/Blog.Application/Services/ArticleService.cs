using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Domain.Entities;
using Blog.DTOs.Article;
using Blog.Infrastructure.Contracts.Interfaces;
using Blog.Infrastructure.Repositories;

namespace Blog.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ArticlesRepository? _articleRepos;
        private readonly TagsRepository? _tagRepos;
        private readonly IMapper _mapper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _articleRepos = unitOfWork.GetRepository<Article>() as ArticlesRepository;
            _tagRepos = unitOfWork.GetRepository<Tag>() as TagsRepository;
            _mapper = mapper;
        }


        public async Task CreateArticleAsync(CreateArticleDTO dto, int authorId)
        {
            var article = _mapper.Map<Article>(dto);    

            article.AuthorId = authorId;
            article.Title = dto.Title;

            await _articleRepos?.Update(article);
        }

        public async Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync()
        {
            var result = await _articleRepos?.GetAll();
            return _mapper.Map<IEnumerable<ArticleDTO>>(result);
        }

        public async Task<ArticleDTO> GetArticleAsync(int id)
        {
            var result = await _articleRepos?.Get(id);
            return _mapper.Map<ArticleDTO>(result);
        }
    }
}

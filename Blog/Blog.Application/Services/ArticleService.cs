using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.Domain.Entities;
using Blog.DTOs.Article;
using Blog.Infrastructure.Contracts.Interfaces;
using Blog.Infrastructure.Repositories;

namespace Blog.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ArticlesRepository _articleRepos;
        private readonly TagsRepository _tagRepos;
        private readonly IMapper _mapper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _articleRepos = unitOfWork.GetRepository<Article>() as ArticlesRepository 
                ?? throw new Exception();
            _tagRepos = unitOfWork.GetRepository<Tag>() as TagsRepository
                ?? throw new Exception();
            _mapper = mapper;
        }


        public async Task<ArticleDTO> GetArticleAsync(int articleId)
        {
            var result = await _articleRepos.Get(articleId);
            return _mapper.Map<ArticleDTO>(result);
        }

        public async Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync()
        {
            var result = await _articleRepos.GetAll();
            return _mapper.Map<IEnumerable<ArticleDTO>>(result);
        }

        public async Task<IEnumerable<ArticleDTO>> GetAllArticlesByAuthorAsync(string authorId)
        {
            var result = await _articleRepos.GetAllByAuthorId(authorId);
            return _mapper.Map<IEnumerable<ArticleDTO>>(result);
        }

        public async Task CreateArticleAsync(CreateArticleDTO dto, string authorId)
        {
            var article = _mapper.Map<Article>(dto);

            if (dto.TagNames != null)
            {
                article.ArticleTags = new List<ArticleTag>();
                foreach (var tagName in dto.TagNames)
                {
                    var tag = await _tagRepos.GetOrCreate(tagName);
                    article.ArticleTags.Add(new ArticleTag { Tag = tag });
                }
            }

            await _articleRepos.Create(article);
        }

        public async Task EditArticleAsync(EditArticleDTO dto, string authorId)
        {
            var article = await _articleRepos.Get(dto.Id)
                ?? throw new NotFoundException($"Статья {dto.Id} не найдена");

            if (article.AuthorId != authorId)
                throw new UnauthorizedAccessException($"Нет прав доступа для редактирования этой статьи");

            article.Title = dto.Title;
            article.Content = dto.Content;
            article.UpdatedAt = DateTime.UtcNow;
            article.IsPublished = dto.IsPublished;

            await _articleRepos.Update(article);
        }

        public async Task DeleteArticleAsync(int articleId)
        {
            var article = await _articleRepos.Get(articleId);
            await _articleRepos.Delete(article);
        }
    }
}

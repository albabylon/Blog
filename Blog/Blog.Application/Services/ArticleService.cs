using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.Domain.Entities;
using Blog.Domain.Identity;
using Blog.DTOs.Article;
using Blog.Infrastructure.Contracts.Interfaces;
using Blog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Blog.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ArticlesRepository _articleRepos;
        private readonly TagsRepository _tagRepos;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _articleRepos = unitOfWork.GetRepository<Article>() as ArticlesRepository 
                ?? throw new Exception();
            _tagRepos = unitOfWork.GetRepository<Tag>() as TagsRepository
                ?? throw new Exception();
            _userManager = userManager
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

        public async Task<IEnumerable<ArticleDTO>> GetAllArticlesByTagAsync(string tagName)
        {
            var result = await _articleRepos.GetAllByTag(tagName);
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
            var user = await _userManager.FindByIdAsync(authorId);
            article.Author = user;
            article.AuthorId = authorId;

            if (dto.TagNames != null)
            {
                article.ArticleTags = new List<ArticleTag>();
                foreach (var tagName in dto.TagNames)
                {
                    var tag = await _tagRepos.GetByName(tagName);
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

            if (dto.TagNames != null)
            {
                var allTagsOfArticle = await _tagRepos.GetAllByArticleId(dto.Id);

                foreach (var tagName in dto.TagNames)
                {
                    if (allTagsOfArticle.Any(x => x.Name == tagName))
                        continue;

                    var tag = await _tagRepos.GetByName(tagName);
                    article.ArticleTags.Add(new ArticleTag { Tag = tag });
                }
            }

            await _articleRepos.Update(article);
        }

        public async Task DeleteArticleAsync(int articleId)
        {
            var article = await _articleRepos.Get(articleId);
            await _articleRepos.Delete(article);
        }
    }
}

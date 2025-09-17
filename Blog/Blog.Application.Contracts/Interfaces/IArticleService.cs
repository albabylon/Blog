using Blog.DTOs.Article;

namespace Blog.Application.Contracts.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDTO> GetArticleAsync(int articleId);
        Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync();
        Task<IEnumerable<ArticleDTO>> GetAllArticlesByAuthorAsync(Guid authorId);
        Task CreateArticleAsync(CreateArticleDTO dto, Guid authorId);
        Task EditArticleAsync(EditArticleDTO dto, Guid authorId);
        Task DeleteArticleAsync(int articleId);
    }
}

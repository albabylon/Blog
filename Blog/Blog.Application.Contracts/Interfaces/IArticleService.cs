using Blog.DTOs.Article;

namespace Blog.Application.Contracts.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDTO> GetArticleAsync(int articleId);
        Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync();
        Task<IEnumerable<ArticleDTO>> GetAllArticlesByAuthorAsync(string authorId);
        Task<IEnumerable<ArticleDTO>> GetAllArticlesByTagAsync(string tagName);
        Task CreateArticleAsync(CreateArticleDTO dto, string authorId);
        Task EditArticleAsync(EditArticleDTO dto, string userId);
        Task DeleteArticleAsync(int articleId, string userId);
    }
}

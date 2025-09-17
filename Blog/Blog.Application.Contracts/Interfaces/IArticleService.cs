using Blog.DTOs.Article;

namespace Blog.Application.Contracts.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDTO> GetArticleAsync(int articleId);
        Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync();
        Task<IEnumerable<ArticleDTO>> GetAllArticlesByAuthorAsync(string authorId);
        Task CreateArticleAsync(CreateArticleDTO dto, string authorId);
        Task EditArticleAsync(EditArticleDTO dto, string authorId);
        Task DeleteArticleAsync(int articleId);
    }
}

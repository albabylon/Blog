using Blog.DTOs.Article;

namespace Blog.Application.Contracts.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDTO> GetArticleAsync(int id);
        Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync();
        Task CreateArticleAsync(CreateArticleDTO dto, int authorId);
    }
}

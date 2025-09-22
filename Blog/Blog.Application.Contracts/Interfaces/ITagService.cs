using Blog.DTOs.Tag;

namespace Blog.Application.Contracts.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagDTO>> GetAllTagsAsync();
        Task<IEnumerable<TagDTO>> GetAllTagsByArticleAsync(int articleId);
        Task CreateArticleAsync(CreateTagDTO dto);
    }
}

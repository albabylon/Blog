using Blog.DTOs.Tag;

namespace Blog.Application.Contracts.Interfaces
{
    public interface ITagService
    {
        Task<TagDTO> GetTagAsync(int tagId);
        Task<IEnumerable<TagDTO>> GetAllTagsAsync();
        Task<IEnumerable<TagDTO>> GetAllTagsByArticleAsync(int articleId);
        Task CreateTagAsync(CreateTagDTO dto);
        Task EditTagAsync(EditTagDTO dto);
        Task DeleteTagAsync(int tagId);
    }
}

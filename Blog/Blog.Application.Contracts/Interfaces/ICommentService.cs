using Blog.DTOs.Comment;

namespace Blog.Application.Contracts.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDTO> GetCommentAsync(int commentId);
        Task<IEnumerable<CommentDTO>> GetAllCommentsAsync();
        Task CreateCommentAsync(CreateCommentDTO dto, int articleId, string authorId);
        Task EditCommentAsync(EditCommentDTO dto, string userId);
        Task DeleteCommentAsync(int commentId, string userId);
    }
}

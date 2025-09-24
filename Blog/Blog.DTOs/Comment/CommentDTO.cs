namespace Blog.DTOs.Comment
{
    public class CommentDTO
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; }
        public string ArticleName { get; set; }
    }
}

namespace Blog.Web.ViewModels.Comment
{
    public class CommentViewModel
    {
        public string Content { get; set; }
        public string User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

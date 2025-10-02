using Blog.Domain.Entities;
using Blog.Web.ViewModels.Tag;

namespace Blog.Web.ViewModels.Article
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PartOfContent { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public IEnumerable<TagViewModel> Tags { get; set; }
        public IEnumerable<string> TagNames { get; set; }
        public bool IsPublished { get; set; } = true;
    }
}

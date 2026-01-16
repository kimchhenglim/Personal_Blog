namespace PersonalBlog.Areas.Admin
{
    public class ArticleVm
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public string Content { get; set; } = "";
    }
}

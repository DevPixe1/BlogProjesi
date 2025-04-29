namespace Blog.Core.Entities
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string AuthorName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Yorumun ait olduğu post
        public Guid PostId { get; set; }
        public Post? Post { get; set; }
    }
}

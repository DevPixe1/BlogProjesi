namespace Blog.Core.DTOs
{
    public class UpdatePostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int CategoryId { get; set; }
    }
}


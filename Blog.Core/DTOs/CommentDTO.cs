namespace Blog.Core.DTOs
{
    public class CommentDto
    {
        public string Content { get; set; }
        public Guid PostId { get; set; }
    }

}

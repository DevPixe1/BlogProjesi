namespace Blog.Core.DTOs
{
    public class CommentDto
    {
        public Guid Id { get; set; }                     // Yorumun benzersiz kimliği
        public string Content { get; set; }              // Yorum içeriği
        public DateTime CreatedAt { get; set; }          // Yorum tarihi
        public Guid PostId { get; set; }                 // Yorumun ait olduğu gönderi
    }
}

namespace Blog.Core.DTOs
{
    // Yeni bir gönderi oluşturmak için kullanılan DTO
    public class CreatePostDto
    {
        public string Title { get; set; } = null!;       // Yeni gönderi başlığı
        public string Content { get; set; } = null!;     // Yeni gönderi içeriği
        public string Author { get; set; } = null!;      // Yeni gönderi yazarı
        public int CategoryId { get; set; }              // Yeni gönderinin kategorisi
    }
}
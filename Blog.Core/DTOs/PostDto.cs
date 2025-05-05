namespace Blog.Core.DTOs
{
    // API'de gönderi verilerini dış dünyaya dönerken kullanılan DTO
    public class PostDto
    {
        public Guid Id { get; set; }                     // Gönderinin benzersiz kimliği
        public string Title { get; set; } = null!;       // Gönderi başlığı
        public string Content { get; set; } = null!;     // Gönderi içeriği
        public string Author { get; set; } = null!;      // Gönderiyi yazan yazar
        public int CategoryId { get; set; }              // Gönderinin ait olduğu kategori
        public DateTime CreatedAt { get; set; }          // Gönderinin oluşturulma tarihi
    }
}
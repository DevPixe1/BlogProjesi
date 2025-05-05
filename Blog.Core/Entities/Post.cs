namespace Blog.Core.Entities
{
    // Blog gönderilerini temsil eden varlık
    public class Post
    {
        public Guid Id { get; set; }                     // Gönderinin benzersiz kimliği
        public string Title { get; set; } = null!;       // Gönderi başlığı
        public string Content { get; set; } = null!;     // Gönderi içeriği
        public string Author { get; set; } = null!;      // Gönderiyi yazan yazar
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Oluşturulma tarihi

        public int CategoryId { get; set; }              // Gönderinin ait olduğu kategori ID’si (yabancı anahtar)
        public Category Category { get; set; } = null!;  // Kategori navigasyon özelliği

        // Gönderiye yapılmış yorumların koleksiyonu
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
namespace Blog.Core.Entities
{
    public class Post
    {
        // Her post için benzersiz kimlik (GUID tipinde)
        public Guid Id { get; set; }

        // Post başlığı
        public string Title { get; set; } = string.Empty;

        // Post içeriği
        public string Content { get; set; } = string.Empty;

        // Oluşturulma tarihi
        public DateTime CreatedAt { get; set; }

        // Postu oluşturan kullanıcıya ait ID (foreign key)
        public Guid UserId { get; set; }

        // Postu oluşturan kullanıcıya ait navigation property
        public User User { get; set; } = null!;

        // Postun ait olduğu kategori ID’si (foreign key)
        public int CategoryId { get; set; }

        // Postun ait olduğu kategori (navigation property)
        public Category Category { get; set; } = null!;

        // Posta ait yorumlar (1-to-many ilişki)
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }


}
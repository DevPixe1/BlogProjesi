namespace Blog.Core.Entities
{
    // Kategorileri temsil eden varlık (örneğin: Teknoloji, Sağlık, Spor)
    public class Category
    {
        public int Id { get; set; }                      // Kategorinin benzersiz kimliği
        public string Name { get; set; } = null!;        // Kategori adı

        // Bu kategoriye ait gönderilerin koleksiyonu
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
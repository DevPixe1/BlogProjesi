namespace Blog.Core.Entities
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        // Bu kategoriye ait yazılar
        public List<Post> Posts { get; set; } = new();
    }
}

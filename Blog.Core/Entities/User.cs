using Blog.Core.Enums;

namespace Blog.Core.Entities
{
    public class User
    {
        // Kullanıcının benzersiz ID’si (GUID)
        public Guid Id { get; set; }

        // Kullanıcı adı
        public string Username { get; set; } = string.Empty;

        // Kullanıcının e-posta adresi
        public string Email { get; set; } = string.Empty;

        // Şifre hash değeri (şifre düz metin olarak tutulmaz)
        public string Password { get; set; } = string.Empty;

        // Kullanıcının rolü (Outsider, User, Author gibi)
        public UserRole Role { get; set; }

        // Kullanıcının yazdığı postlar (1-to-many ilişki)
        public ICollection<Post> Posts { get; set; } = new List<Post>();

        // Kullanıcının yaptığı yorumlar (1-to-many ilişki)
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

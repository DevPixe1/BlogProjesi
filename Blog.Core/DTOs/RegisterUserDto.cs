using Blog.Core.Enums;

namespace Blog.Core.DTOs
{
    // Yeni kullanıcı kaydı için gerekli alanları içerir
    public class RegisterUserDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserRole Role { get; set; } // User ya da Author seçimi
    }
}
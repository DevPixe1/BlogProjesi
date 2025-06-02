using Blog.Core.Enums;

namespace Blog.Core.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty; // Kullanıcı adı
        public string Email { get; set; } = string.Empty; // Kullanıcıdan gelen mail
        public string Password { get; set; } = string.Empty; // Kullanıcıdan gelen düz şifre
        public UserRole Role { get; set; } // Kullanıcı rolü
    }
}

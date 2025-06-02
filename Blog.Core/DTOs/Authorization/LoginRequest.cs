namespace Blog.Core.DTOs.Authorization
{
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty; // Kullanıcı adı
        public string Password { get; set; } = string.Empty; //Şifre
    }
}

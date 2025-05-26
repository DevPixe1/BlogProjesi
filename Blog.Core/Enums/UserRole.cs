
namespace Blog.Core.Enums
{
    public enum UserRole
    {
        Outsider = 0, // sadece GET işlemleri
        User = 1,     // GET + yorum ekleme
        Author = 2    // tüm işlemler
    }
}
using System.Security.Cryptography;
using System.Text;

namespace Blog.Service.Helpers
{
    // Şifreleri hashlemek ve doğrulamak için yardımcı sınıf
    public static class PasswordHasher
    {
        // Şifreyi SHA256 algoritması ile hashler
        public static string Hash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        // Girilen şifre ile hashlenmiş şifreyi karşılaştırır
        public static bool Verify(string password, string hashedPassword)
        {
            return Hash(password) == hashedPassword;
        }
    }
}

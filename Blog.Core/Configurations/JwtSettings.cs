namespace Blog.Core.Configurations
{
    // Uygulamanın JWT (JSON Web Token) ayarlarını tutan konfigürasyon sınıfı.
    // Bu ayarlar, güvenli token üretimi ve doğrulamasında kullanılmaktadır.
    public class JwtSettings
    {
        // JWT belirteci oluşturulurken kullanılacak gizli anahtar.
        // Bu değer, token imzalama işlemi için önemlidir.
        public string SecretKey { get; set; } = null!;

        // Token'in yayıncısını belirtir.
        // Genellikle uygulamanın veya organizasyonun adı kullanılmaktadır.
        public string Issuer { get; set; } = null!;

        // Token'in hedef kitlesini (alıcı) belirtir.
        // Bu, token doğrulamasında kullanılan bir parametredir.
        public string Audience { get; set; } = null!;

        // Token'in geçerlilik süresini dakika cinsinden belirler.
        // Token süresi dolduğunda, kullanıcıdan yeniden token talep edilmesi gerekir.
        public int ExpireMinutes { get; set; }
    }
}

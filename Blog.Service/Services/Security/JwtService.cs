using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Core.Configurations;
using Blog.Core.Enums;
using Blog.Core.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;

    // Uygulamadaki JWT ayarlarını alır (appsettings.json'dan)
    public JwtService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    // JWT üretir, kullanıcı adı ve rol bilgisi alır
    public string GenerateToken(string username, UserRole role)
    {
        // Token'a eklenecek bilgileri hazırla (kullanıcı adı + rol)
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role.ToString()) // Rol bilgisi buraya eklenir
        };

        // Şifreleme için gizli anahtar hazırlanır
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        // HMAC SHA256 algoritması ile token imzalanır
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Token nesnesi oluşturulur (issuer, audience, süre, claim, imza)
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
            signingCredentials: creds
        );

        // Token string olarak döndürülür
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

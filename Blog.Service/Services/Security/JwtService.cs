using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Core.Configurations;
using Blog.Core.DTOs;
using Blog.Core.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;

    public JwtService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }
    public string GenerateToken(UserInfoDto user)
    {
        // Kullanıcı bilgileri üzerinden JWT claim'leri oluşturur
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Kullanıcı ID
            new Claim("UserId", user.Id.ToString()), // Alternatif kullanıcı ID
            new Claim(ClaimTypes.Name, user.Username), // Kullanıcı adı
            new Claim(ClaimTypes.Email, user.Email), // E-posta adresi
            new Claim(ClaimTypes.Role, user.Role.ToString()) // Kullanıcı rolü
        };

        // JWT imzalama anahtarını oluşturur
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // JWT belirteci oluşturur
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer, // Token yayıncısı
            audience: _jwtSettings.Audience, // Token hedef kitlesi
            claims: claims, // Kullanıcıya ait claim'ler
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes), // Token geçerlilik süresi
            signingCredentials: creds // İmzalama bilgileri
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

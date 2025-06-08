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

    public JwtService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    // Artık userId parametresi yok, sadece username ve role alıyor
    public string GenerateToken(string username, UserRole role)
    {
        // Eğer gerçekten username boş gelirse hata fırlatabilirsiniz
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("GenerateToken çağrılırken username boş olamaz.");

        var claims = new List<Claim>
        {
            // Kullanıcının adı
            new Claim(ClaimTypes.Name, username),

            // Kullanıcının rolü (örneğin "Author", "User", vb.)
            new Claim(ClaimTypes.Role, role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

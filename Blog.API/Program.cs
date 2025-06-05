using Blog.API.Extensions;
using Blog.Service.Mapping;
using Blog.Service.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;
using Blog.Service.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Blog.Core.Configurations;
using Blog.Core.Interfaces;
using Blog.Service.Services; // JwtService
using Microsoft.OpenApi.Models;
using Blog.Core.Repositories;
using Blog.Data.Repositories;
using Blog.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Proje servislerini merkezi olarak ekler (DbContext, AutoMapper, FluentValidation, Repos, Services, UnitOfWork vs.)
builder.Services.AddProjectServices(builder.Configuration);

// Kullanýcý Servisi & HttpContextAccessor**
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// JWT ayarlarýný appsettings.json'dan okur ve DI container'a ekler
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Yetkilendirme servisini ekler (rollerin çalýþmasý için gereklidir)
builder.Services.AddAuthorization();

// JWT authentication middleware'i ekler
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
    if (jwtSettings == null)
    {
        throw new InvalidOperationException("JWT ayarlarý bulunamadý, lütfen appsettings.json dosyanýzý kontrol edin.");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        ClockSkew = TimeSpan.Zero // Token süresi dolunca hemen geçersiz olsun
    };
});

// FluentValidation konfigürasyonu
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePostDtoValidator>(); // BURASI ÖNEMLÝ

// AutoMapper konfigürasyonu
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Controller ve Swagger servisleri
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // JWT ile kimlik doðrulama için Swagger yapýlandýrmasý
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT token'ýnýzý 'Bearer {token}' formatýnda girin.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchema, new[] { "Bearer" } }
    });
});

var app = builder.Build();

// Geliþtirme ortamýnda Swagger'ý aktif eder
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // JWT doðrulama iþlemini aktive eder
app.UseAuthorization();  // Yetki kontrolü burada devreye girer

app.MapControllers();

app.Run();

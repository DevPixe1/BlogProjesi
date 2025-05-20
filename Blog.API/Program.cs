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

var builder = WebApplication.CreateBuilder(args);

// Proje servislerini merkezi olarak ekler (DbContext, AutoMapper, FluentValidation, Repos, Services, UnitOfWork vs.)
builder.Services.AddProjectServices(builder.Configuration);

// JWT ayarlar�n� appsettings.json'dan okur ve DI container'a ekler
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// JwtService'i IOC container'a ekler
builder.Services.AddScoped<IJwtService, JwtService>();

// JWT authentication middleware'i ekler
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        ClockSkew = TimeSpan.Zero // Token s�resi dolunca hemen ge�ersiz olsun
    };
});

// FluentValidation konfig�rasyonu
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePostDtoValidator>(); // BURASI �NEML�

// AutoMapper konfig�rasyonu
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Controller ve Swagger servisleri
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Geli�tirme ortam�nda Swagger'� aktif eder
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// JWT do�rulama i�lemini aktive eder
app.UseAuthentication();

// Yetki kontrol� (�rne�in [Authorize]) burada devreye girer
app.UseAuthorization();

app.MapControllers();

app.Run();

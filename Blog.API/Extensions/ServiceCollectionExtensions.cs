using AutoMapper;
using Blog.Core.Repositories;
using Blog.Core.Services;
using Blog.Core.UnitOfWork;
using Blog.Data;
using Blog.Data.Repositories;
using Blog.Data.UnitOfWork;
using Blog.Service.Mapping;
using Blog.Service.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Blog.API.Extensions
{
    // Uygulamadaki servisleri merkezi olarak kaydeder
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Veritabanı bağlantısı ve DbContext konfigürasyonu
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // AutoMapper konfigürasyonu
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // FluentValidation ayarları
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            // Generic repository kaydı
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // UnitOfWork kaydı
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Servislerin DI kaydı
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();

            // Swagger konfigürasyonu (JWT desteği ile birlikte)
            services.AddSwaggerGen(c =>
            {
                // Swagger dokümanı başlığı ve versiyonu
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Blog API",
                    Version = "v1",
                    Description = "JWT ile korunan Blog API dokümantasyonu"
                });

                // JWT kimlik doğrulama şeması tanımı
                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Bearer {token} formatında JWT giriniz.",
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

                // Swagger'a şemayı tanıt
                c.AddSecurityDefinition("Bearer", securitySchema);

                // Tüm endpoint'lerde güvenlik gereksinimi olarak JWT kullan
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                });
            });

            return services;
        }
    }
}

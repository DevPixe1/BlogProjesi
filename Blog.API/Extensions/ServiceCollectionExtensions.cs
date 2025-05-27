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
using Blog.Service.Validations;
using Microsoft.OpenApi.Models;
using Blog.Core.Interfaces;

namespace Blog.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Uygulama servislerini Dependency Injection konteynerine ekler
        public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Veritabanı bağlantısını ve EF Core ayarlarını yapılandırır
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // AutoMapper profillerini yükler (nesne eşleme işlemleri için)
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // FluentValidation otomatik validasyon middleware'ini aktif eder
            services.AddFluentValidationAutoValidation();

            // FluentValidation için istemci tarafı desteklerini ekler
            services.AddFluentValidationClientsideAdapters();

            // FluentValidation validator'larını bu assembly içinden bulur
            services.AddValidatorsFromAssemblyContaining<CreatePostDtoValidator>();

            // Generic repository yapısını DI konteynerine ekler
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Unit of Work pattern’ini ekler (birden fazla repository’yi yönetmek için)
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Post işlemleri için servis sınıfını tanımlar
            services.AddScoped<IPostService, PostService>();

            // Yorum işlemleri için servis sınıfını tanımlar
            services.AddScoped<ICommentService, CommentService>();

            // JWT token üretim servisini tanımlar
            services.AddScoped<IJwtService, JwtService>();

            // Yetkilendirme servislerini aktif eder
            services.AddAuthorization();

            // Swagger (API dokümantasyonu) yapılandırması
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Blog API",
                    Version = "v1",
                    Description = "JWT ile korunan Blog API dokümantasyonu"
                });

                // Swagger için JWT güvenlik şeması tanımı
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

                // Swagger'a "Bearer" güvenlik şeması eklenir
                c.AddSecurityDefinition("Bearer", securitySchema);

                // Swagger'da tüm endpointler için Bearer token zorunluluğu eklenir
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                });
            });

            // Hizmet koleksiyonu geriye döndürülür
            return services;
        }
    }
}


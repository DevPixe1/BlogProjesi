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

            return services;
        }
    }
}

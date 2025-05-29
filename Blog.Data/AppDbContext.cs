using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    // Uygulamanın EF Core üzerinden veritabanı ile etkileşim kurduğu ana sınıf.
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // Entity'ler için DbSet tanımlamaları
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<User> Users => Set<User>(); // Kullanıcılar için DbSet tanımı

        // Fluent API ile konfigürasyonlar ve seed veriler
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Kategoriye başlangıç verileri ekleniyor
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Roman" },
                new Category { Id = 2, Name = "Bilim" },
                new Category { Id = 3, Name = "Tarih" }
            );

            // Post entity konfigürasyonu
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Title).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Content).IsRequired();
                entity.Property(p => p.CreatedAt).IsRequired();

                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Posts)
                      .HasForeignKey(p => p.CategoryId);

                // Yeni: User ile ilişki kuruluyor
                entity.HasOne(p => p.User)
                      .WithMany(u => u.Posts)
                      .HasForeignKey(p => p.UserId);
            });


            // Comment entity konfigürasyonu
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Content).IsRequired();
                entity.Property(c => c.CreatedAt).IsRequired();

                // Bir yorum bir posta aittir
                entity.HasOne(c => c.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.PostId);
            });

            // User entity konfigürasyonu
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id); // Primary key
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50); // Kullanıcı adı zorunlu
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);   // E-posta zorunlu
                entity.Property(u => u.PasswordHash).IsRequired();              // Şifre hash'i zorunlu
                entity.Property(u => u.Role).IsRequired();                      // Rol zorunlu

                // E-posta tekil olmalı
                entity.HasIndex(u => u.Email).IsUnique();
            });
        }
    }
}

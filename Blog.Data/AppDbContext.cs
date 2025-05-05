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
                entity.Property(p => p.Author).IsRequired().HasMaxLength(100);
                entity.Property(p => p.CreatedAt).IsRequired();

                // Bir post bir kategoriye aittir
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Posts)
                      .HasForeignKey(p => p.CategoryId);
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
        }
    }
}

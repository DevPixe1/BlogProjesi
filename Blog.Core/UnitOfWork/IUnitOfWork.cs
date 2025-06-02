using Blog.Core.Entities;
using Blog.Core.Repositories;

namespace Blog.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Post> Posts { get; } // Post entity için generic repository
        IGenericRepository<Comment> Comments { get; } // Comment entity için generic repository
        IGenericRepository<Category> Categories { get; } // Category entity için generic repository
        IUserRepository Users { get; } // Kullanıcı işlemleri için özel repository
        Task<int> SaveChangesAsync(); // Yapılan tüm değişiklikleri veritabanına kaydeder
    }
}

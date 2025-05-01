using Blog.Core.Entities;

namespace Blog.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Post> Posts { get; }
        IGenericRepository<Comment> Comments { get; }
        IGenericRepository<Category> Categories { get; }
        Task<int> SaveChangesAsync();
    }
}

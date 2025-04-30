using Blog.Core.Repositories;

namespace Blog.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        Task<int> SaveChangesAsync();
    }
}

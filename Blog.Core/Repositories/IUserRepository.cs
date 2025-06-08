using Blog.Core.Entities;
using System.Linq.Expressions;

namespace Blog.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByUsernameAndPasswordAsync(string username, string password);
        Task<bool> AnyAsync(Expression<Func<User, bool>> predicate);

    }
}

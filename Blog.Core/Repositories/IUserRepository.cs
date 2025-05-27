using Blog.Core.Entities;

namespace Blog.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByUsernameAndPasswordAsync(string username, string password);
    }
}

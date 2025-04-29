using Blog.Core.Entities;

namespace Blog.Core.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<Post?> GetPostWithCommentsAsync(Guid id);
    }
}

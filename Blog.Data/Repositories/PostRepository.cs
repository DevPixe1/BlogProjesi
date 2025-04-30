using Blog.Core.Entities;
using Blog.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Post?> GetPostWithCommentsAsync(Guid id)
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

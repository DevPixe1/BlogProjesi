using Blog.Core.Repositories;
using Blog.Core.UnitOfWork;
using Blog.Data.Repositories;

namespace Blog.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IPostRepository _postRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _postRepository = new PostRepository(context);
        }

        public IPostRepository Posts => _postRepository;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

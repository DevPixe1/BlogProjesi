using System.Threading.Tasks;
using Blog.Core.Entities;
using Blog.Core.Repositories;
using Blog.Core.UnitOfWork;
using Blog.Data.Repositories;

namespace Blog.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<Post> Posts { get; }
        public IGenericRepository<Comment> Comments { get; }
        public IGenericRepository<Category> Categories { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Posts = new GenericRepository<Post>(_context);
            Comments = new GenericRepository<Comment>(_context);
            Categories = new GenericRepository<Category>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

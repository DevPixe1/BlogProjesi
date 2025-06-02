using Blog.Core.Entities;
using Blog.Core.Repositories;
using Blog.Core.UnitOfWork;
using Blog.Data.Repositories;

namespace Blog.Data.UnitOfWork
{
    // Uygulama genelinde tüm repository işlemlerinin yönetildiği sınıf.
    // Tek bir SaveChanges çağrısıyla birden fazla işlem birlikte kaydedilir.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // İlgili repository'lerin property'leri
        public IGenericRepository<Post> Posts { get; }
        public IGenericRepository<Comment> Comments { get; }
        public IGenericRepository<Category> Categories { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            // Her entity için generic repository örnekleri oluşturuluyor
            Posts = new GenericRepository<Post>(_context);
            Comments = new GenericRepository<Comment>(_context);
            Categories = new GenericRepository<Category>(_context);
            Users = new UserRepository(context);

        }

        // Değişiklikleri veritabanına kaydeder
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

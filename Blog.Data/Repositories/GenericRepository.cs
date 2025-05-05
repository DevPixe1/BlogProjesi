using Blog.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Data.Repositories
{
    // Generic repository implementasyonu: EF Core kullanarak CRUD işlemleri
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;  // Veritabanı bağlamı
        private readonly DbSet<T> _dbSet;        // Generic DbSet (tablo)

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); // ✅ DbSet burada başlatılıyor
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }

}

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
            _dbSet = _context.Set<T>(); // DbSet burada başlatılıyor
        }

        // Tüm kayıtları getirir
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // ID'ye göre tek bir kaydı getirir
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Yeni bir kayıt ekler
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Var olan bir kaydı günceller
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        // Var olan bir kaydı siler
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        // Belirli bir filtreye göre kayıtları getirir
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

    }
}

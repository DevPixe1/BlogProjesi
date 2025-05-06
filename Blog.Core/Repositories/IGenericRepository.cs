using System.Linq.Expressions;

namespace Blog.Core.Repositories
{
    // Generic repository arayüzü: Tüm entity'ler için ortak veri erişim işlemleri
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();         // Tüm verileri getirir
        Task<T?> GetByIdAsync(Guid id);      // ID ile tek bir veri getirir
        Task AddAsync(T entity);             // Yeni veri ekler
        void Update(T entity);               // Var olan veriyi günceller
        void Delete(T entity);               // Veriyi siler

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync();
    }
}
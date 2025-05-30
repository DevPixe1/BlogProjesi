using Blog.Core.DTOs;

namespace Blog.Core.Services
{
    // Post işlemlerine özel iş mantığını tanımlayan servis arayüzü
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllAsync();                    // Tüm postları DTO olarak getirir
        Task<PostDto?> GetByIdAsync(Guid id);                        // Belirli bir postu ID ile getirir
        Task<Guid> CreateAsync(CreatePostDto dto, Guid userId);      // Yeni bir post oluşturur (UserId eklendi)
        Task<bool> UpdateAsync(Guid id, UpdatePostDto dto);          // Var olan bir postu günceller
        Task<bool> DeleteAsync(Guid id);                             // Belirli bir postu siler
    }
}

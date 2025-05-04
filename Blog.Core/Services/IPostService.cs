using Blog.Core.DTOs;

namespace Blog.Core.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllAsync();
        Task<PostDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(CreatePostDto dto);
        Task<bool> UpdateAsync(Guid id, UpdatePostDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

using Blog.Core.DTOs;

namespace Blog.Core.Services
{
    public interface ICommentService
    {
        // Yeni bir yorum ekler.
        Task AddCommentAsync(CreateCommentDto dto);
        Task UpdateCommentAsync(Guid commentId, UpdateCommentDto dto);
        Task DeleteCommentAsync(Guid commentId);
        // Belirli bir gönderiye (post) ait tüm yorumları getirir.
        // ICommentService.cs
        Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(Guid postId);

    }

}

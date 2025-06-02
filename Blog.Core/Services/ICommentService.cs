using Blog.Core.DTOs;

namespace Blog.Core.Services
{
    public interface ICommentService
    {
        Task AddCommentAsync(CreateCommentDto dto); // Yorum ekleme
        Task UpdateCommentAsync(Guid commentId, UpdateCommentDto dto); // Yorum güncelleme
        Task DeleteCommentAsync(Guid commentId); // Yorum silme
        Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(Guid postId); // Belirli gönderiye ait yorumları getirme
    }
}


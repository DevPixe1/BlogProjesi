using Blog.Core.DTOs;
using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public interface ICommentService
    {
        // Yeni bir yorum ekler.
        Task AddCommentAsync(CommentDto dto);

        // Belirli bir gönderiye (post) ait tüm yorumları getirir.
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(Guid postId);
    }

}

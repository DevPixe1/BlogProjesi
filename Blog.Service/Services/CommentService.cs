using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Repositories;
using Blog.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<Comment> _commentRepository;

        public CommentService(IGenericRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task AddCommentAsync(CommentDto dto)
        {
            // Yeni yorum nesnesi oluşturuluyor
            var comment = new Comment
            {
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow, // Şu anki zaman kaydediliyor
                PostId = dto.PostId          // Yorumun ait olduğu blog post ID'si
            };

            // Yorum veritabanına ekleniyor
            await _commentRepository.AddAsync(comment);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            // Belirli bir blog post'a ait tüm yorumlar getiriliyor
            return await _commentRepository.GetAllAsync(c => c.PostId == postId);
        }
    }
}

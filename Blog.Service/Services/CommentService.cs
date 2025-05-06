using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Repositories;
using Blog.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Service.Services
{
    // Yorumlarla ilgili işlemleri yöneten servis sınıfı
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<Comment> _commentRepository;

        public CommentService(IGenericRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // Yeni bir yorum ekler
        public async Task AddCommentAsync(CommentDto dto)
        {
            var comment = new Comment
            {
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow,
                PostId = dto.PostId
            };

            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync(); // Kaydetmeyi unutma!
        }

        // Var olan bir yorumu günceller
        public async Task UpdateCommentAsync(Guid commentId, string newContent)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
                throw new Exception("Yorum bulunamadı.");

            comment.Content = newContent;
            _commentRepository.Update(comment);
            await _commentRepository.SaveChangesAsync();
        }

        // Belirtilen ID'ye sahip bir yorumu siler
        public async Task DeleteCommentAsync(Guid commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
                throw new Exception("Yorum bulunamadı.");

            _commentRepository.Delete(comment);
            await _commentRepository.SaveChangesAsync();
        }

        // Belirli bir posta ait tüm yorumları döner
        public async Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(Guid postId)
        {
            var comments = await _commentRepository.GetAllAsync(c => c.PostId == postId);

            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                PostId = c.PostId
            });
        }

    }
}

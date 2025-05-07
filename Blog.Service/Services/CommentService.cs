using AutoMapper;
using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Repositories;
using Blog.Core.Services;

namespace Blog.Service.Services
{
    // Yorumlarla ilgili iş mantığını yöneten servis sınıfı (AutoMapper ile çalışır)
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(IGenericRepository<Comment> commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        // Yeni bir yorum ekler
        public async Task AddCommentAsync(CreateCommentDto dto)
        {
            var comment = _mapper.Map<Comment>(dto);
            comment.CreatedAt = DateTime.UtcNow;
            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync();
        }

        // Mevcut bir yorumu günceller
        public async Task UpdateCommentAsync(Guid commentId, UpdateCommentDto dto)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
                throw new Exception("Yorum bulunamadı.");

            comment.Content = dto.Content;
            _commentRepository.Update(comment);
            await _commentRepository.SaveChangesAsync();
        }

        // Belirtilen ID'ye sahip yorumu siler
        public async Task DeleteCommentAsync(Guid commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
                throw new Exception("Yorum bulunamadı.");

            _commentRepository.Delete(comment);
            await _commentRepository.SaveChangesAsync();
        }

        // Belirli bir posta ait tüm yorumları DTO listesi olarak döner
        public async Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(Guid postId)
        {
            var comments = await _commentRepository.GetAllAsync(c => c.PostId == postId);
            return _mapper.Map<IEnumerable<CommentDto>>(comments);  // Entity - DTO dönüşümü
        }
    }
}

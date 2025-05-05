using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Services;
using Blog.Core.UnitOfWork;

namespace Blog.Service.Services
{
    // Post ile ilgili iş mantıklarının bulunduğu servis sınıfı.
    // DTO kullanarak dış dünyaya sade veri sunar.
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ID ile post getirir, DTO'ya dönüştürür
        public async Task<PostDto?> GetByIdAsync(Guid id)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(id);
            if (post == null) return null;

            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Author = post.Author,
                CategoryId = post.CategoryId,
                CreatedAt = post.CreatedAt
            };
        }

        // Tüm postları DTO listesi olarak döner
        public async Task<IEnumerable<PostDto>> GetAllAsync()
        {
            var posts = await _unitOfWork.Posts.GetAllAsync();
            return posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Author = p.Author,
                CategoryId = p.CategoryId,
                CreatedAt = p.CreatedAt
            });
        }

        // Yeni bir post oluşturur ve ID’sini döner
        public async Task<Guid> CreateAsync(CreatePostDto dto)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Content = dto.Content,
                Author = dto.Author,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Posts.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();
            return post.Id;
        }

        // Postu günceller, işlem başarılıysa true döner
        public async Task<bool> UpdateAsync(Guid id, UpdatePostDto dto)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(id);
            if (post == null) return false;

            post.Title = dto.Title;
            post.Content = dto.Content;
            post.Author = dto.Author;
            post.CategoryId = dto.CategoryId;

            _unitOfWork.Posts.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        // Postu siler, işlem başarılıysa true döner
        public async Task<bool> DeleteAsync(Guid id)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(id);
            if (post == null) return false;

            _unitOfWork.Posts.Delete(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}

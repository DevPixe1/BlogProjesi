using AutoMapper;
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
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ID ile post getirir, DTO'ya dönüştürür
        public async Task<PostDto?> GetByIdAsync(Guid id)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(id);
            if (post == null) return null;

            return _mapper.Map<PostDto>(post);
        }

        // Tüm postları DTO listesi olarak döner
        public async Task<IEnumerable<PostDto>> GetAllAsync()
        {
            var posts = await _unitOfWork.Posts.GetAllAsync();
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        // **************** Güncellenmiş CreateAsync Metodu ****************
        // Artık CreateAsync metoduna, JWT token'dan çekilen kullanıcı bilgilerini (userId, username)
        // parametre olarak alarak, post oluşturma işleminde bu bilgileri ilgili alanlara atıyoruz.
        public async Task<Guid> CreateAsync(CreatePostDto dto, Guid userId, string username)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Content = dto.Content,
                CategoryId = dto.CategoryId,
                UserId = userId,    // Token’dan alınan kullanıcı ID'si
                Author = username,  // Token’dan alınan kullanıcı adı
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

            _mapper.Map(dto, post); // DTO'dan mevcut post nesnesine değerleri aktar
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

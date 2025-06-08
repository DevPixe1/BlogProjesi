using AutoMapper;
using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Services;
using Blog.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Service.Services
{
    // Post ile ilgili iş mantıklarının bulunduğu servis sınıfı.
    // DTO kullanarak dış dünyaya sade veri sunar.
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        // IUserService parametresi eklenerek _userService alanı initialize ediliyor.
        public PostService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
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

        // Artık CreateAsync metoduna sadece DTO ve username alıyor; GUID (UserId) dışarıdan gelmiyor.
        public async Task<Guid> CreateAsync(CreatePostDto dto, string username)
        {
            var user = await _userService.GetByUsernameAsync(username);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Content = dto.Content,
                CategoryId = dto.CategoryId,
                UserId = user.Id, // Veritabanındaki geçerli kullanıcı id'si
                Author = user.Username,
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
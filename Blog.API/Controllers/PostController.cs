using Blog.Core.Enums;
using Blog.Service.Authorization;
using Blog.Core.DTOs;
using Blog.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICurrentUserService _currentUserService;

        public PostController(IPostService postService, ICurrentUserService currentUserService)
        {
            _postService = postService;
            _currentUserService = currentUserService;
        }

        // Tüm blog gönderilerini getirir
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        // Belirli bir gönderiyi GUID'e göre getirir
        [HttpGet("{guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid guid)
        {
            var post = await _postService.GetByIdAsync(guid);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        // Yeni bir gönderi oluşturur
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
        {
            // Debug için token içerisindeki tüm claim'leri yazdır
            Console.WriteLine("=== Token Claims ===");
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            if (_currentUserService.UserId == Guid.Empty || string.IsNullOrEmpty(_currentUserService.Username))
                return Unauthorized("Geçersiz kullanıcı bilgisi.");

            var postId = await _postService.CreateAsync(dto, _currentUserService.UserId, _currentUserService.Username);
            return CreatedAtAction(nameof(GetById), new { guid = postId }, null);
        }


        // Var olan bir gönderiyi günceller
        [HttpPut("{guid}")]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> Update(Guid guid, [FromBody] UpdatePostDto dto)
        {
            var result = await _postService.UpdateAsync(guid, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        // Belirli bir gönderiyi siler
        [HttpDelete("{guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid guid)
        {
            try
            {
                // Role kontrolü artık ICurrentUserService üzerinden okunuyor
                RoleAuthorizationService.EnsureUserHasPermission(_currentUserService.Role, UserRole.Author);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

            var result = await _postService.DeleteAsync(guid);
            if (!result)
                return NotFound();
            return NoContent();
        }

    }
}

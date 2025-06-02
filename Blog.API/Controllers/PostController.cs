using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
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
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
        {
            Guid userId;
            try
            {
                userId = GetUserIdFromToken(); // JWT belirtecinden geçerli bir kullanıcı kimliği çıkar.
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var postId = await _postService.CreateAsync(dto, userId);
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
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await _postService.DeleteAsync(guid);
            if (!result)
                return NotFound();
            return NoContent();
        }

        // JWT belirtecinden kullanıcı kimliğini çıkarmak için kullanılır.
        // Bu metodda, kullanıcının "NameIdentifier" ya da "UserId" claim'leri aranır. 
        // Eğer bu claim'lerden hiçbiri bulunamazsa veya claim değeri geçerli bir GUID formatında değilse,
        // ilgili hata mesajı ile bir istisna fırlatılır.
        // Metod, geçerli bir kullanıcı GUID'si döndürür.
        private Guid GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?? User.Claims.FirstOrDefault(c => c.Type == "UserId");

            if (userIdClaim == null)
            {
                throw new Exception("Token içinde kullanıcı tanımlayıcı bilgisi bulunamadı.");
            }

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new Exception("Kullanıcı tanımlayıcı bilgisi geçerli bir GUID formatında değil.");
            }

            return userId;
        }
    }
}

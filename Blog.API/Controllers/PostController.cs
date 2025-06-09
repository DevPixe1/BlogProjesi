using Blog.Core.DTOs;
using Blog.Core.Interfaces;
using Blog.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

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

        // Gönderiyi GUID'e göre getirir
        [HttpGet("{guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid guid)
        {
            var post = await _postService.GetByIdAsync(guid);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        // Yeni gönderi oluşturur (sadece Author rolündeki kullanıcılar)
        [HttpPost]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
        {
            // Log: Token içerisindeki claim'leri yazdırıyoruz. (Debug amacıyla)
            Console.WriteLine("=== Token Claims ===");
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            // Token'dan username bilgisini çekiyoruz.
            var authorUsername = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(authorUsername))
                return Unauthorized("Token’da Name claim’i bulunamadı.");

            // Post servisine username’i gönderiyoruz.
            var postId = await _postService.CreateAsync(dto, authorUsername);
            return CreatedAtAction(nameof(GetById), new { guid = postId }, "Gönderi başarıyla oluşturuldu.");

        }

        // Var olan gönderiyi günceller (sadece Author rolü)
        [HttpPut("{guid}")]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> Update(Guid guid, [FromBody] UpdatePostDto dto)
        {
            var result = await _postService.UpdateAsync(guid, dto);
            if (!result)
                return NotFound();
            return Ok("Gönderi başarıyla güncellendi.");
        }


        // Gönderiyi siler (sadece Author rolü)
        [HttpDelete("{guid}")]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await _postService.DeleteAsync(guid);
            if (!result)
                return NotFound();
            return Ok("Gönderi başarıyla silindi.");
        }
    }
}

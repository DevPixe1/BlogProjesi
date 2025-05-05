using Blog.Core.DTOs;
using Blog.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        // PostController, blog gönderileriyle ilgili istekleri yöneten API denetleyicisidir.
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // Tüm blog gönderilerini getirir
        [HttpGet("Hepsini getir")]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        // Belirli bir gönderiyi ID'sine göre getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null) return NotFound(); // Gönderi bulunamazsa 404 döner
            return Ok(post);
        }

        // Yeni bir gönderi oluşturur
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
        {
            var postId = await _postService.CreateAsync(dto);
            // Başarıyla oluşturulduğunda 201 Created döner, yeni gönderinin URL'ini içerir
            return CreatedAtAction(nameof(GetById), new { id = postId }, null);
        }

        // Var olan bir gönderiyi günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePostDto dto)
        {
            var result = await _postService.UpdateAsync(id, dto);
            if (!result) return NotFound(); // Güncellenecek gönderi bulunamazsa 404 döner
            return NoContent(); // Başarılı güncellemede içerik dönülmez
        }

        // Belirli bir gönderiyi siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _postService.DeleteAsync(id);
            if (!result) return NotFound(); // Silinecek gönderi yoksa 404 döner
            return NoContent(); // Başarılı silme işleminde içerik dönülmez
        }
    }
}

using Blog.Core.DTOs;
using Blog.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Blog.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        
        [HttpPost] // Yeni yorum ekleme endpoint'i
        public async Task<IActionResult> PostComment([FromBody] CreateCommentDto dto)
        {
            await _commentService.AddCommentAsync(dto);
            return Ok("Yorum başarıyla eklendi.");
        }
        
        [HttpPut("{guid}")] // Var olan bir yorumu güncelleme endpoint'i
        public async Task<IActionResult> UpdateComment(Guid guid, [FromBody] UpdateCommentDto dto)
        {
            await _commentService.UpdateCommentAsync(guid, dto);
            return Ok("Yorum güncellendi.");
        }
        
        [HttpDelete("{guid}")] // Belirli bir yorumu silme endpoint'i
        public async Task<IActionResult> DeleteComment(Guid guid)
        {
            await _commentService.DeleteCommentAsync(guid);
            return Ok("Yorum silindi.");
        }

        [HttpGet("post/{PostGuid}")] // Belirli bir gönderiye ait tüm yorumları listeleme
        public async Task<IActionResult> GetComments(Guid PostGuid)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(PostGuid);
            return Ok(comments);
        }
    }
}

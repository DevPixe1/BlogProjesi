using Blog.Core.DTOs;
using Blog.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // API adresi: api/comments şeklinde olacak
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        // ICommentService dependency injection ile alınır
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost] // Yeni yorum ekleme endpoint'i
        public async Task<IActionResult> PostComment([FromBody] CommentDto dto)
        {
            await _commentService.AddCommentAsync(dto); // Yorumu veritabanına kaydeder
            return Ok("Yorum başarıyla eklendi.");
        }
        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateComment(Guid guid, [FromBody] string content)
        {
            await _commentService.UpdateCommentAsync(guid, content);
            return Ok("Yorum güncellendi.");
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteComment(Guid guid)
        {
            await _commentService.DeleteCommentAsync(guid);
            return Ok("Yorum silindi.");
        }


        [HttpGet("post/{PostGuid}")] // Belirli bir post'a ait yorumları getirir
        public async Task<IActionResult> GetComments(Guid PostGuid)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(PostGuid);
            return Ok(comments);
        }
    }
}

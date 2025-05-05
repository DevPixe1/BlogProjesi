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

        [HttpGet("post/{postId}")] // Belirli bir post'a ait yorumları getirir
        public async Task<IActionResult> GetComments(Guid postId)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }
    }
}

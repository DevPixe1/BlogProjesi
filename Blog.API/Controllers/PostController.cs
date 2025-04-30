using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Repositories;
using Blog.Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto)
        {
            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Posts.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { post.Id });
        }
    }
}

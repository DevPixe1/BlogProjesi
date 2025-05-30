﻿using Blog.Core.DTOs;
using Blog.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        [HttpGet]
        [AllowAnonymous] // Herkes erişebilir (Outsider dahil)
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        // Belirli bir gönderiyi GUID'e göre getirir
        [HttpGet("{guid}")]
        [AllowAnonymous] // Herkes erişebilir (Outsider dahil)
        public async Task<IActionResult> GetById(Guid guid)
        {
            var post = await _postService.GetByIdAsync(guid);
            if (post == null) return NotFound(); // Gönderi bulunamazsa 404 döner
            return Ok(post);
        }

        // Yeni bir gönderi oluşturur
        [HttpPost]
        [Authorize(Roles = "Author")] // Sadece Author oluşturabilir
        public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
        {
            var userId = GetUserIdFromToken(); // JWT'den userId çekiliyor
            var postId = await _postService.CreateAsync(dto, userId); // userId ile birlikte gönderiliyor
            return CreatedAtAction(nameof(GetById), new { guid = postId }, null);
        }

        // Var olan bir gönderiyi günceller
        [HttpPut("{guid}")]
        [Authorize(Roles = "Author")] // Sadece Author güncelleyebilir
        public async Task<IActionResult> Update(Guid guid, [FromBody] UpdatePostDto dto)
        {
            var result = await _postService.UpdateAsync(guid, dto);
            if (!result) return NotFound(); // Güncellenecek gönderi bulunamazsa 404 döner
            return NoContent(); // Başarılı güncellemede içerik dönülmez
        }

        // Belirli bir gönderiyi siler
        [HttpDelete("{guid}")]
        [Authorize(Roles = "Author")] // Sadece Author silebilir
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await _postService.DeleteAsync(guid);
            if (!result) return NotFound(); // Silinecek gönderi yoksa 404 döner
            return NoContent(); // Başarılı silme işleminde içerik dönülmez
        }
        private Guid GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
        }

    }
}

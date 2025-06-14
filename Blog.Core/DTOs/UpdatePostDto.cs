﻿namespace Blog.Core.DTOs
{
    // Var olan bir gönderiyi güncellemek için kullanılan DTO
    public class UpdatePostDto
    {
        public string Title { get; set; } = null!;       // Güncellenmiş başlık
        public string Content { get; set; } = null!;     // Güncellenmiş içerik
        public int CategoryId { get; set; }              // Güncellenmiş kategori
    }
}
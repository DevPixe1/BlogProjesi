using System;

namespace Blog.Core.DTOs
{
    // Yeni bir yorum oluşturmak için kullanılan DTO
    public class CreateCommentDto
    {
        public string Content { get; set; }              // Yorum içeriği
        public Guid PostId { get; set; }                 // Yorumun ait olduğu post
    }
}

using System;

namespace Blog.Core.Entities
{
    public class Comment
    {
        public Guid Id { get; set; } // Guid
        public string Content { get; set; } // Yorum içeriği
        public DateTime CreatedAt { get; set; } // Gönderilme tarihi

        // İlişki: Her yorum bir posta ait
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}

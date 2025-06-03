using AutoMapper;
using Blog.Core.DTOs;
using Blog.Core.Entities;

namespace Blog.Service.Mapping
{
    // Entity - DTO dönüşümlerini tanımlar
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Post entity'si ile PostDto arasında çift yönlü dönüşüm
            CreateMap<Post, PostDto>().ReverseMap();

            // Yeni bir gönderi oluşturmak için kullanılan CreatePostDto'dan Post entity'sine dönüşüm
            CreateMap<CreatePostDto, Post>();

            // Mevcut bir gönderiyi güncellemek için kullanılan UpdatePostDto'dan Post entity'sine dönüşüm
            CreateMap<UpdatePostDto, Post>();

            // Yorum entity'si ile CommentDto arasında çift yönlü dönüşüm
            CreateMap<Comment, CommentDto>().ReverseMap();

            // Yeni bir yorum oluşturmak için kullanılan CreateCommentDto'dan Comment entity'sine dönüşüm
            CreateMap<CreateCommentDto, Comment>();

            // Mevcut bir yorumu güncellemek için kullanılan UpdateCommentDto'dan Comment entity'sine dönüşüm
            CreateMap<UpdateCommentDto, Comment>();

            // Kullanıcı kayıt/giriş işlemleri için dönüşüm.
            // UserDto nesnesinden User entity'sine dönüşüm yapılır (şifre ve rol bilgilerini içerir).
            CreateMap<UserDto, User>();
        }
    }
}

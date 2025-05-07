using Blog.Core.DTOs;
using FluentValidation;

namespace Blog.Service.Validations
{
    // CreatePostDto için doğrulama kuralları
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik boş olamaz.");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Yazar boş olamaz.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori seçilmelidir.");
        }
    }
}

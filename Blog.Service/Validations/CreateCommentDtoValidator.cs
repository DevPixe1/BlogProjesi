using Blog.Core.DTOs;
using FluentValidation;

namespace Blog.Service.Validators
{
    // Yorum oluşturma işlemleri için doğrulama kuralları
    public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Yorum içeriği boş olamaz.")
                .MaximumLength(500).WithMessage("Yorum en fazla 500 karakter olabilir.");

            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Yorumun ait olduğu postId boş olamaz.");
        }
    }
}

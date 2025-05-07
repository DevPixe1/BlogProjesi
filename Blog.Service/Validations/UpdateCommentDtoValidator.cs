using Blog.Core.DTOs;
using FluentValidation;

namespace Blog.Service.Validators
{
    // Yorum güncelleme işlemleri için doğrulama kuralları
    public class UpdateCommentDtoValidator : AbstractValidator<UpdateCommentDto>
    {
        public UpdateCommentDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Yorum içeriği boş olamaz.")
                .MaximumLength(500).WithMessage("Yorum en fazla 500 karakter olabilir.");
        }
    }
}

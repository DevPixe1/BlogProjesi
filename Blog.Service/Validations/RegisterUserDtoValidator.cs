using FluentValidation;
using Blog.Core.DTOs;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Kullanıcı adı gereklidir.")
            .Length(3, 20).WithMessage("Kullanıcı adı 3 ile 20 karakter arasında olmalıdır.")
            .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Kullanıcı adı yalnızca harf ve rakam içerebilir.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta adresi gereklidir.")
            .EmailAddress().WithMessage("Geçersiz e-posta formatı.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre gereklidir.")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
            .Matches(@"\d").WithMessage("Şifre en az bir rakam içermelidir.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Geçersiz rol seçimi.");
    }
}

namespace SS.Application.SiteSettings.CreateSiteSetting;
public class CreateSiteSettingCommandValidator : AbstractValidator<CreateSiteSettingCommand>
{
    public CreateSiteSettingCommandValidator()
    {
        RuleFor(_ => _.Title).NotEmpty()
            .WithName("عنوان")
            .WithMessage(ValidationMessage.NotEmpty)
            .MaximumLength(75).WithMessage(ValidationMessage.MaximumLength);

        RuleFor(_ => _.Description).NotEmpty()
            .WithName("توضیحات")
            .WithMessage(ValidationMessage.NotEmpty)
            .MaximumLength(255).WithMessage(ValidationMessage.MaximumLength);

        RuleFor(_ => _.Address).NotEmpty()
            .WithName("آدرس")
            .WithMessage(ValidationMessage.NotEmpty)
            .MaximumLength(1000).WithMessage(ValidationMessage.MaximumLength);

        RuleFor(_ => _.MobilePhoneNumber).NotEmpty()
            .WithName("شماره موبایل")
            .WithMessage(ValidationMessage.NotEmpty)
            .Matches(RegularExpressionConsts.PhoneNumber).WithMessage(ValidationMessage.RegexIsInvalid);

        RuleFor(_ => _.NationalId).NotEmpty()
            .WithName("شناسه ملی")
            .WithMessage(ValidationMessage.NotEmpty)
            .Matches(RegularExpressionConsts.NationalCode).WithMessage(ValidationMessage.RegexIsInvalid);
    }
}
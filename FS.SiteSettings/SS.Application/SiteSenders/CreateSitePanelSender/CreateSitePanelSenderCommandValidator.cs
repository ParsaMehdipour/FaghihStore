namespace SS.Application.SiteSenders.CreateSitePanelSender;

public class CreateSitePanelSenderCommandValidator : AbstractValidator<CreateSitePanelSenderCommand>
{
    public CreateSitePanelSenderCommandValidator()
    {
        RuleFor(_ => _.UserName).NotEmpty()
            .WithName("نام کاربری")
            .WithMessage(ValidationMessage.NotEmpty)
            .MaximumLength(75).WithMessage(ValidationMessage.MaximumLength);

        RuleFor(_ => _.Password).NotEmpty()
            .WithName("رمز عبور")
            .WithMessage(ValidationMessage.NotEmpty)
            .MaximumLength(150).WithMessage(ValidationMessage.MaximumLength);
    }
}

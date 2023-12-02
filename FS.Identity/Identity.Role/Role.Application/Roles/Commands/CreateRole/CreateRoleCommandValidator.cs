namespace Role.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator(IRoleRepository roleRepository)
    {
        Include(new NameValidator(roleRepository));

        Include(new DisplayNameValidator(roleRepository));
    }
}

internal class NameValidator : AbstractValidator<CreateRoleCommand>
{
    public NameValidator(IRoleRepository roleRepository, bool isForModify = false)
    {
        RuleFor(_ => _.Name)
            .MaximumLength(50).WithName("نام نقش").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (name, cancellationToken) => !await roleRepository.NameBeUnique(Guid.Empty, name, isForModify, cancellationToken)).WithMessage(ValidationMessage.BeUnique);
    }
}

internal class DisplayNameValidator : AbstractValidator<CreateRoleCommand>
{
    public DisplayNameValidator(IRoleRepository roleRepository, bool isForModify = false)
    {
        RuleFor(_ => _.DisplayName)
            .MaximumLength(50).WithName("نام نمایشی").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (displayName, cancellationToken) => !await roleRepository.DisplayNameBeUnique(Guid.Empty, displayName, isForModify, cancellationToken)).WithMessage(ValidationMessage.BeUnique);
    }
}
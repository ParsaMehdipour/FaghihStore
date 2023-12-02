namespace Role.Application.Roles.Commands.EditRole;

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator(IRoleRepository roleRepository)
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");

        RuleFor(_ => _.Name)
            .MaximumLength(50).WithName("نام نقش").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (command, name, cancellationToken) => !await roleRepository.NameBeUnique(command.Id, command.Name, true, cancellationToken)).WithMessage(ValidationMessage.BeUnique);

        RuleFor(_ => _.DisplayName)
            .MaximumLength(50).WithName("نام نمایشی").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (command, displayName, cancellationToken) => !await roleRepository.DisplayNameBeUnique(command.Id, command.DisplayName, true, cancellationToken)).WithMessage(ValidationMessage.BeUnique);
    }
}

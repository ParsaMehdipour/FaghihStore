using Category.Domain.Repositories;
using FluentValidation;
using SH.Infrastructure.Consts;

namespace Category.Application.Categories.Commands.EditCategory;

public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    public EditCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");

        RuleFor(_ => _.Title)
            .MaximumLength(100).WithName("عنوان").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (command, title, cancellationToken) => !await categoryRepository.TitleExists(command.Id, command.Title, true, cancellationToken)).WithMessage(ValidationMessage.NotExists).WithName("عنوان");

        RuleFor(_ => _.Slug)
            .MaximumLength(100).WithName("Slug").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (command, slug, cancellationToken) => !await categoryRepository.SlugExists(command.Id, command.Slug, true, cancellationToken)).WithMessage(ValidationMessage.NotExists).WithName("عنوان");

        //RuleFor(_ => _.OrderNumber)
        //    .NotEqual(0).WithName("اولویت").WithMessage(ValidationMessage.NotEqual)
        //    .MustAsync(async (command, orderNumber, cancellationToken) => !await categoryRepository.OrderNumberExists(command.Id, command.OrderNumber, true, cancellationToken)).WithMessage(ValidationMessage.BeUnique);

        //RuleFor(_ => _.ParentId)
        //    .MustAsync(async (command, parent, cancellationToken) => !await categoryRepository.ParentExists(command.Id, command.ParentId, true, cancellationToken))
        //    .WithMessage(ValidationMessage.NotExists).WithName("زیر مجموعه");
    }
}

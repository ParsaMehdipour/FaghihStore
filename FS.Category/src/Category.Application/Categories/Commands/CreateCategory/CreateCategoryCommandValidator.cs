using Category.Domain.Repositories;

using FluentValidation;

using SH.Infrastructure.Consts;

namespace Category.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator(ICategoryRepository repository)
    {
        Include(new TitleValidator(repository));
        Include(new SlugValidator(repository));
        //Include(new OrderNumberValidator(repository));
        //Include(new ParentIdValidator(repository));
    }
}

internal class TitleValidator : AbstractValidator<CreateCategoryCommand>
{
    public TitleValidator(ICategoryRepository repository, bool isForModify = false)
    {
        RuleFor(_ => _.Title)
            .MaximumLength(100).WithName("عنوان").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (title, cancellationToken) => !await repository.TitleExists(Guid.Empty, title, isForModify, cancellationToken)).WithMessage(ValidationMessage.BeUnique);
    }
}

internal class SlugValidator : AbstractValidator<CreateCategoryCommand>
{
    public SlugValidator(ICategoryRepository repository, bool isForModify = false)
    {
        RuleFor(_ => _.Slug)
            .MaximumLength(100).WithName("Slug").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (slug, cancellationToken) => !await repository.SlugExists(Guid.Empty, slug, isForModify, cancellationToken)).WithMessage(ValidationMessage.BeUnique);
    }
}

internal class OrderNumberValidator : AbstractValidator<CreateCategoryCommand>
{
    public OrderNumberValidator(ICategoryRepository repository, bool isForModify = false)
    {
        RuleFor(_ => _.OrderNumber)
            .NotEqual(0).WithName("اولویت").WithMessage(ValidationMessage.NotEqual)
            .MustAsync(async (orderNumber, cancellationToken) => !await repository.OrderNumberExists(Guid.Empty, orderNumber, isForModify, cancellationToken)).WithMessage(ValidationMessage.BeUnique);
    }
}

internal class ParentIdValidator : AbstractValidator<CreateCategoryCommand>
{
    public ParentIdValidator(ICategoryRepository repository, bool isForModify = false)
    {
        RuleFor(_ => _.ParentId)
            .MustAsync(async (parentId, cancellationToken) => !await repository.ParentExists(Guid.Empty, parentId, isForModify, cancellationToken)).WithMessage(ValidationMessage.BeUnique);

    }
}

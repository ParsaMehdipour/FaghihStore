using SS.Application.Interfaces;
using SS.Application.SiteSenders.CreateSitePanelSender;
using SS.Application.SiteSenders.EditSitePanelSender;

namespace SS.Application.SiteSenders;

public class SitePanelSenderValidatorService : ISitePanelSenderValidatorService
{
    public IValidator<CreateSitePanelSenderCommand> CreateValidator { get; set; }
    public IValidator<EditSitePanelSenderCommand> EditValidator { get; set; }

    //public SitePanelSenderValidatorService(IValidator<CreateSitePanelSenderCommand> createValidator, IValidator<EditSitePanelSenderCommand> editValidator)
    //{
    //    CreateValidator = createValidator;
    //    EditValidator = editValidator;
    //}

    public SitePanelSenderValidatorService(IValidator<CreateSitePanelSenderCommand> createValidator)
    {
        CreateValidator = createValidator;
    }

    public async Task<Result> CreateValidation(CreateSitePanelSenderCommand command, CancellationToken cancellationToken)
    {
        var result = await CreateValidator.ValidateAsync(command, cancellationToken);

        if (result.IsValid is false)
            return Result.Fail(result.Errors.Select(_ => _.ErrorMessage).FirstOrDefault());

        return Result.Ok();
    }

    public async Task<Result> EditValidation(EditSitePanelSenderCommand command, CancellationToken cancellationToken)
    {
        return await this.CreateValidation(new(command.UserName,
            command.Password,
            command.SenderType,
            command.Status), cancellationToken);
    }
}

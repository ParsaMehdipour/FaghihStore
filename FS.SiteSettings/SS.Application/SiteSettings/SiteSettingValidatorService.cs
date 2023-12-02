using SS.Application.Interfaces;
using SS.Application.SiteSettings.CreateSiteSetting;
using SS.Application.SiteSettings.EditSiteSetting;

namespace SS.Application.SiteSettings;
internal class SiteSettingValidatorService : ISiteSettingValidatorService
{
    public IValidator<CreateSiteSettingCommand> CreateValidator { get; set; }
    //public IValidator<EditSiteSettingCommand> EditValidator { get; set; }

    //public SiteSettingValidatorService(IValidator<CreateSiteSettingCommand> createValidator,
    //    IValidator<EditSiteSettingCommand> editValidator)
    //{
    //    CreateValidator = createValidator;
    //    EditValidator = editValidator;
    //}

    public SiteSettingValidatorService(IValidator<CreateSiteSettingCommand> createValidator)
    {
        CreateValidator = createValidator;
    }

    public async Task<Result> CreateValidation(CreateSiteSettingCommand command, CancellationToken cancellationToken)
    {
        var result = await CreateValidator.ValidateAsync(command, cancellationToken);

        if (result.IsValid is false)
            return Result.Fail(result.Errors.Select(_ => _.ErrorMessage).FirstOrDefault());

        return Result.Ok();
    }

    public async Task<Result> EditValidation(EditSiteSettingCommand command, CancellationToken cancellationToken)
    {
        return await this.CreateValidation(new(command.Title,
                                                                   command.Description,
                                                                   command.MobilePhoneNumber,
                                                                   command.Phone,
                                                                   command.NationalId,
                                                                   command.Address), cancellationToken);
    }
}
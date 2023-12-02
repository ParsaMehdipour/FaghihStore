using SS.Application.SiteSettings.CreateSiteSetting;
using SS.Application.SiteSettings.EditSiteSetting;

namespace SS.Application.Interfaces;
public interface ISiteSettingValidatorService
{
    public IValidator<CreateSiteSettingCommand> CreateValidator { get; set; }
    //public IValidator<EditSiteSettingCommand> EditValidator { get; set; }

    Task<Result> CreateValidation(CreateSiteSettingCommand command, CancellationToken cancellationToken);
    Task<Result> EditValidation(EditSiteSettingCommand command, CancellationToken cancellationToken);
}
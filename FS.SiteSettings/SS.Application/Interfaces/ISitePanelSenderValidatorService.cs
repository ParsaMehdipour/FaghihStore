using SS.Application.SiteSenders.CreateSitePanelSender;
using SS.Application.SiteSenders.EditSitePanelSender;

namespace SS.Application.Interfaces;

public interface ISitePanelSenderValidatorService
{
    public IValidator<CreateSitePanelSenderCommand> CreateValidator { get; set; }
    public IValidator<EditSitePanelSenderCommand> EditValidator { get; set; }

    Task<Result> CreateValidation(CreateSitePanelSenderCommand command, CancellationToken cancellationToken);
    Task<Result> EditValidation(EditSitePanelSenderCommand command, CancellationToken cancellationToken);
}

using SS.Application.Interfaces;
using SS.Application.SiteSenders.CreateSitePanelSender;
using SS.Application.SiteSenders.EditSitePanelSender;
using SS.Domain.Enums;
using SS.Domain.Models;

namespace SS.Application.SiteSenders;

public class SitePanelSenderService : ISitePanelSenderService
{

    protected ISitePanelSenderRepository _SitePanelSenderRepository { get; }
    protected ISitePanelSenderValidatorService _SitePanelSenderValidatorService { get; }
    protected SitePanelSenderFactory _SitePanelSenderFactory { get; }

    public SitePanelSenderService(ISitePanelSenderRepository sitePanelSenderRepository, ISitePanelSenderValidatorService sitePanelSenderValidatorService, SitePanelSenderFactory sitePanelSenderFactory)
    {
        _SitePanelSenderRepository = sitePanelSenderRepository;
        _SitePanelSenderValidatorService = sitePanelSenderValidatorService;
        _SitePanelSenderFactory = sitePanelSenderFactory;
    }

    public async Task<Result<EditSitePanelSenderCommand>> Single(CancellationToken cancellationToken)
    {
        SitePanelSender sitePanelSender = await _SitePanelSenderRepository.GetFirst(cancellationToken);

        if (sitePanelSender is null)
        {
            var createResult = await this.Add(new("نام کاربری", "123456789", SenderType.Sms, true), cancellationToken);

            if (createResult.IsFailed)
                return Result.Fail(createResult.Errors.Select(_ => _.Message).FirstOrDefault());

            sitePanelSender = createResult.Value;

            return Result.Ok(sitePanelSender.ToCommand());
        }

        return Result.Ok(sitePanelSender.ToCommand());
    }

    public async Task<Result<SitePanelSender>> Add(CreateSitePanelSenderCommand command, CancellationToken cancellationToken)
    {
        var result = await _SitePanelSenderValidatorService.CreateValidation(command, cancellationToken);

        if (result.IsFailed)
            return Result.Fail(result.Errors.Select(_ => _.Message).FirstOrDefault());

        SitePanelSender sitePanelSender = _SitePanelSenderFactory.Create(command.UserName, command.Password,
            command.SenderType, command.IsEnabled);

        await _SitePanelSenderRepository.AddAsync(sitePanelSender, cancellationToken);
        await _SitePanelSenderRepository.SaveAsync(cancellationToken);

        return Result.Ok(sitePanelSender);
    }

    public async Task<Result> Edit(EditSitePanelSenderCommand command, CancellationToken cancellationToken)
    {
        var result = await _SitePanelSenderValidatorService.EditValidation(command, cancellationToken);

        if (result.IsFailed)
            return Result.Fail(result.Errors.Select(_ => _.Message).FirstOrDefault());

        SitePanelSender sitePanelSender = await _SitePanelSenderRepository.GetFirst(cancellationToken);

        if (sitePanelSender is null)
            return Result.Fail("SitePanelSender Not found");

        sitePanelSender.SetUsername(command.UserName);
        sitePanelSender.SetPassword(command.Password);
        sitePanelSender.SetSenderType(command.SenderType);
        sitePanelSender.SetPanelSenderStatus(command.Status);

        await _SitePanelSenderRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }

    public Task<Result> Delete(Guid Id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
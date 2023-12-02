using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SH.Infrastructure.Criteria;
using VG.Api.Controllers.Shared;
using VG.Application.Criteria;
using VG.Application.VarietyGroups.Queries.GetVarietyGroups;

namespace VG.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class VarietyGroupController : BaseApiController
{
    public VarietyGroupController(IMediator mediator) : base(mediator)
    {

    }

    [HttpGet]
    public async Task<ActionResult<Result<ResponseModel<IEnumerable<GetVarietyGroupDto>>>>> Get([FromQuery] VarietyGroupQueryStringParameter parameters, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request: new GetVarietyGroupsQuery(parameters), cancellationToken);

        return Ok(response);
    }
}
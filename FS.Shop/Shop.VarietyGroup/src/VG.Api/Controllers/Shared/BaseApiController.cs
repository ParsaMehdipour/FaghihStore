using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VG.Api.Controllers.Shared;

public class BaseApiController : ControllerBase
{
    protected IMediator _mediator { get; }

    public BaseApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
}

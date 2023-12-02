using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Category.Api.Controllers.Shared;
public class BaseApiController : ControllerBase
{
    protected IMediator _mediator { get; }

    public BaseApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public BaseApiController()
    {

    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VG.Api.Controllers.Shared;
using VG.Application.Varieties.Queries.GetInventoriesVarities;

namespace VG.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class VarietyController : BaseApiController
{
    public VarietyController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// retrieve specific inventory's variety
    /// </summary>
    /// <param name="varietyDictionaryString"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BadHttpRequestException"></exception>
    [HttpGet("{varietyDictionaryString}")]
    public async Task<ActionResult<Dictionary<Guid, string>>> GetInventoriesVarieties(string varietyDictionaryString, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(varietyDictionaryString))
            return BadRequest();

        Dictionary<Guid, Guid> categoryDictionary = JsonSerializer.Deserialize<Dictionary<Guid, Guid>>(varietyDictionaryString) ?? throw new BadHttpRequestException("Request Failed");

        var response = await _mediator.Send(new GetInventoriesVarietiesQuery(categoryDictionary), cancellationToken);

        return response;
    }
}
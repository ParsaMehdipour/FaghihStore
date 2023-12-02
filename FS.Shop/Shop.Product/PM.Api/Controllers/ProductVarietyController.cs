using MediatR;
using Microsoft.AspNetCore.Mvc;
using PM.Api.Controllers.Shared;
using PM.Application.ProductVarieties.Commands.CreateProductVariety;
using PM.Application.ProductVarieties.Queries.GetProductsWithVarieties;
using PM.Application.ProductVarieties.Queries.GetProductVarietiesWithInventories;
using System.Text.Json;

namespace PM.Api.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ProductVarietyController : BaseApiController
{
    public ProductVarietyController(IMediator mediator) : base(mediator)
    {

    }

    /// <summary>
    /// Create a product variety
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProductVariety(CreateProductVarietyCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return result.Value;
    }

    /// <summary>
    /// retrieve specific inventory's product title and variety title
    /// </summary>
    /// <param name="productVarietyDictionaryString"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{productVarietyDictionaryString}")]
    public async Task<ActionResult<List<GetProductsWithVarietiesDto>>> GetProductsWithVarieties([FromRoute] string productVarietyDictionaryString, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(productVarietyDictionaryString))
            return BadRequest();

        Dictionary<Guid, Guid> productVarietyDictionary = JsonSerializer.Deserialize<Dictionary<Guid, Guid>>(productVarietyDictionaryString) ?? throw new BadHttpRequestException("Request Failed");

        var result = await _mediator.Send(new GetProductsWithVarietiesQuery(productVarietyDictionary), cancellationToken);

        return result.Value;
    }

    [HttpGet("{queryJson}")]
    public async Task<ActionResult<List<GetProductVarietiesWithInventoriesDto>>> GetProductVarietiesWithInventories([FromRoute] string queryJson, CancellationToken cancellationToken)
    {
        var query = new GetProductVarietiesWithInventoriesQuery(Guid.Parse(queryJson));

        var result = await _mediator.Send(query, cancellationToken);

        return result.Value;
    }
}

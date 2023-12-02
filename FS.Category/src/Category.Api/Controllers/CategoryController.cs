using Category.Api.Controllers.Shared;
using Category.Application.Categories.Queries.GetProductCategories;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace Category.Api.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CategoryController : BaseApiController
{
    public CategoryController(IMediator mediator) : base(mediator)
    {

    }

    /// <summary>
    /// retrieve specific product's category
    /// </summary>
    /// <param name="categoryDictionaryString"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{categoryDictionaryString}")]
    public async Task<ActionResult<Dictionary<Guid, string>>> GetProductCategories(string categoryDictionaryString, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(categoryDictionaryString))
            return BadRequest();

        Dictionary<Guid, Guid> categoryDictionary = JsonSerializer.Deserialize<Dictionary<Guid, Guid>>(categoryDictionaryString) ?? throw new BadHttpRequestException("Request Failed");

        var response = await _mediator.Send(new GetProductCategoriesQuery(categoryDictionary), cancellationToken);

        return response;
    }

    /// <summary>
    /// return category Model that need a category title by Id:GUID
    /// </summary>
    /// <param name="categoryDictionaryString"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BadHttpRequestException"></exception>
    [HttpGet("{categoryDictionaryString}")]
    public async Task<ActionResult<Dictionary<Guid, string>>> GetOwnModelCategories(string categoryDictionaryString, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(categoryDictionaryString))
            return BadRequest();

        Dictionary<Guid, Guid> categoryDictionary = JsonSerializer.Deserialize<Dictionary<Guid, Guid>>(categoryDictionaryString) ?? throw new BadHttpRequestException("Request Failed");

        var response = await _mediator.Send(new GetOwnTitleRecordCategoriesQuery(categoryDictionary), cancellationToken);

        if (response.IsFailed)
            return BadRequest(response.Errors);

        return response.Value;
    }
}

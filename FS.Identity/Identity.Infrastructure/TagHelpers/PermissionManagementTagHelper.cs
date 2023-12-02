using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Identity.Infrastructure.TagHelpers;

[HtmlTargetElement("a")]
[HtmlTargetElement("div")]
[HtmlTargetElement("li")]
[HtmlTargetElement("ul")]
[HtmlTargetElement("td")]
[HtmlTargetElement("form")]
public class PermissionManagementTagHelper : TagHelper
{
    [HtmlAttributeName("identity-permission")]
    public string Permission { get; set; }

    private IAuthorizationService _authorizationService { get; }
    private HttpContext _httpContext { get; }

    public PermissionManagementTagHelper(IAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor)
    {
        _authorizationService = authorizationService;
        _httpContext = httpContextAccessor.HttpContext;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (string.IsNullOrWhiteSpace(Permission) is false)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContext.User, Permission);

            if (authorizationResult.Succeeded is false)
            {
                output.SuppressOutput();
                return;
            }
        }

        await base.ProcessAsync(context, output);
    }
}
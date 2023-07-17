using System.ComponentModel.DataAnnotations;
using hexagonal.API.Modules.Common;
using hexagonal.API.Modules.Common.FeatureFlags;
using hexagonal.Application.Bases;
using hexagonal.Application.Components.CategoryComponent.Queries;
using hexagonal.Application.Paginations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace hexagonal.API.Controllers;

// [Authorize]
[FeatureGate(CustomFeature.Category)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryQuery _categoryQuery;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryQuery categoryQuery, ILogger<CategoryController> logger)
    {
        _categoryQuery = categoryQuery;
        _logger = logger;
    }

    [HttpGet("get-all")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQuery? paginationQuery)
    {
        try
        {
            var result = await _categoryQuery.GetAll(paginationQuery).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [HttpGet("get-all-dropdown")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
    public async Task<IActionResult> GetAllDropdown()
    {
        try
        {
            var result = await _categoryQuery.GetAllDropdown().ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    /// <summary>
    ///     Get an category details.
    /// </summary>
    /// <param name="categoryId"></param>
    [HttpGet("get-by-id/{categoryId:int}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
    public async Task<IActionResult> GetById([FromRoute] [Required] int categoryId)
    {
        try
        {
            var result = await _categoryQuery.GetByIdDefault(categoryId).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}
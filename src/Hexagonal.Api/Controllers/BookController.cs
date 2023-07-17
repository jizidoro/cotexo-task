using System.ComponentModel.DataAnnotations;
using hexagonal.API.Modules.Common;
using hexagonal.API.Modules.Common.FeatureFlags;
using hexagonal.Application.Bases;
using hexagonal.Application.Components.BookComponent.Commands;
using hexagonal.Application.Components.BookComponent.Contracts;
using hexagonal.Application.Components.BookComponent.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace hexagonal.API.Controllers;

// [Authorize]
[FeatureGate(CustomFeature.Book)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookCommand _bookCommand;
    private readonly IBookQuery _bookQuery;
    private readonly ILogger<BookController> _logger;

    public BookController(IBookCommand bookCommand,
        IBookQuery bookQuery, ILogger<BookController> logger)
    {
        _bookCommand = bookCommand;
        _bookQuery = bookQuery;
        _logger = logger;
    }

    [HttpGet("get-all")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _bookQuery.GetAll().ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    /// <summary>
    ///     Get an book details.
    /// </summary>
    /// <param name="bookId"></param>
    [HttpGet("get-by-id/{bookId:int}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
    public async Task<IActionResult> GetById([FromRoute][Required] int bookId)
    {
        try
        {
            var result = await _bookQuery.GetByIdDefault(bookId).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [Authorize]
    [HttpPost("create")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
    public async Task<IActionResult> Create([FromBody][Required] BookCreateDto dto)
    {
        try
        {
            var result = await _bookCommand.Create(dto).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [Authorize]
    [HttpPut("edit")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Edit))]
    public async Task<IActionResult> Edit([FromBody][Required] BookEditDto dto)
    {
        try
        {
            var result = await _bookCommand.Edit(dto).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }

    [Authorize]
    [HttpDelete("delete/{bookId:int}")]
    [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
    public async Task<IActionResult> Delete([FromRoute][Required] int bookId)
    {
        try
        {
            var result = await _bookCommand.Delete(bookId).ConfigureAwait(false);
            return StatusCode(result.Code, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new SingleResultDto<EntityDto>(e));
        }
    }
}
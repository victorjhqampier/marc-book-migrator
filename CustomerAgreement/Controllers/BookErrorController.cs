using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAgreement.Controllers;

[Route("api/book/error")]
[ApiController]
public class BookErrorController : ControllerBase
{
    private readonly ILogger<BookErrorController> _logger;
    private readonly IBookMigrateApplication _appBook;

    public BookErrorController(IBookMigrateApplication appBook, ILogger<BookErrorController> logger)
    {
        _appBook = appBook;
        _logger = logger;
    }

    [HttpGet("total")]
    public async Task<IActionResult> GetTotalAsync()
    {
        try
        {
            return Ok();
            //return StatusCode(result.StatusCode, result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"api/book/proccess ::: Message > {ex.Message} ::: Input >");
            return StatusCode(500, EasyResponseBianHelper.EasyInternalErrorRespond(10098));
        }
    }

    [HttpGet("detail")]
    public async Task<IActionResult> GetDetailAsync()
    {
        try
        {
            //var result = await _appBook.GetVariableAsync();
            //return StatusCode(result.StatusCode, result);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"api/book/reproccess ::: Message > {ex.Message} ::: Input >");
            return StatusCode(500, EasyResponseBianHelper.EasyInternalErrorRespond(10098));
        }
    }
}

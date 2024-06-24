using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAgreement.Controllers;

[Route("api/book/success")]
[ApiController]
public class BookSuccessController : ControllerBase
{
    private readonly ILogger<BookSuccessController> _logger;
    private readonly IBookMigrateApplication _appBook;

    public BookSuccessController(IBookMigrateApplication appBook, ILogger<BookSuccessController> logger)
    {
        _appBook = appBook;
        _logger = logger;
    }

    [HttpGet("total")]
    public async Task<IActionResult> GetTotalAsync()
    {
        try
        {
            //var result = await _appBook.GetVariableAsync();
            //return StatusCode(result.StatusCode, result);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"api/book/success ::: Message > {ex.Message} ::: Input >");
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
            _logger.LogError($"api/book/success ::: Message > {ex.Message} ::: Input >");
            return StatusCode(500, EasyResponseBianHelper.EasyInternalErrorRespond(10098));
        }
    }
}

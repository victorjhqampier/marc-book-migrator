using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerAgreement.Controllers;

[Route("api/book/warning")]
[ApiController]
public class BookWarningController : ControllerBase
{
    private readonly ILogger<BookWarningController> _logger;
    private readonly IBookMigrateApplication _appBook;

    public BookWarningController(IBookMigrateApplication appBook, ILogger<BookWarningController> logger)
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
            _logger.LogError($"api/book/warning ::: Message > {ex.Message} ::: Input >");
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
            _logger.LogError($"api/book/warning ::: Message > {ex.Message} ::: Input >");
            return StatusCode(500, EasyResponseBianHelper.EasyInternalErrorRespond(10098));
        }
    }
}

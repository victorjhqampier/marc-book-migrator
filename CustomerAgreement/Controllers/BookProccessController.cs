using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAgreement.Controllers;

[Route("api/book/proccess")]
[ApiController]
public class BookProccessController : ControllerBase
{
    private readonly ILogger<BookProccessController> _logger;
    private readonly IBookMigrateApplication _appBook;

    public BookProccessController(IBookMigrateApplication appBook, ILogger<BookProccessController> logger)
    {
        _appBook = appBook;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> ProccessAsync()
    {
        try
        {
            var result = await _appBook.ProcessAsync();
            return StatusCode(result.StatusCode, result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"api/book/proccess ::: Message > {ex.Message} ::: Input >");
            return StatusCode(500, EasyResponseBianHelper.EasyInternalErrorRespond(10098));
        }
    }

    [HttpPut("reproccess")]
    public async Task<IActionResult> ReproccessAsync()
    {
        try
        {
            var result = await _appBook.ReprocessAsync();
            return StatusCode(result.StatusCode, result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"api/book/reproccess ::: Message > {ex.Message} ::: Input >");
            return StatusCode(500, EasyResponseBianHelper.EasyInternalErrorRespond(10098));
        }
    }
}

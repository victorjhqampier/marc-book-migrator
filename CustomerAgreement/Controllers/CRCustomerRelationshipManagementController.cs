using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CustomerAgreement.Controllers;

[Route("customer-agreement/v1.0/customer-relationship-management")]
[ApiController]
public class CRCustomerRelationshipManagementController : ControllerBase
{   
    private readonly ILogger<CRCustomerRelationshipManagementController> _logger;
    private readonly ILoggerCase _tracker;
    private readonly IBookMigrateApplication _appBook;

    public CRCustomerRelationshipManagementController(IBookMigrateApplication appBook, ILogger<CRCustomerRelationshipManagementController> logger, ILoggerCase tracker)
    {
        _appBook = appBook;
        _logger = logger;
        _tracker = tracker;
    }

    [HttpGet("data-complete/retrieve")]
    async public Task<IActionResult> Get()
    {        
        try
        {
            var result = await _appBook.GetVariableAsync();
            return StatusCode(result.StatusCode, result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"data-complete/retrieve ::: Message > {ex.Message} ::: Input >");            
            return StatusCode(500, EasyResponseBianHelper.EasyInternalErrorRespond(10098));
        }
    }
}

using Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAgreement.Controllers;

[Route("/")]
[ApiController]

public class IndexController : ControllerBase
{
    [Authorize(Policy = "ReadScope")]
    [HttpGet]
    public IActionResult GetInfo()
    {
        return Ok(EasyResponseHelper.EasySuccessRespond(new {info = "Template for System Layer API", status="Service Running ..." }));
    }
}

using CreditCardValidator_API.Common;
using CreditCardValidator_API.Model;
using CreditCardValidator_API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardValidator_API.Controllers;

[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[Route("api/[controller]")]
[ApiController]
public class CreditCardController : ControllerBase
{
    private readonly ICreditCardValidationService _validationService;
    public CreditCardController(ICreditCardValidationService validationService)
    {
        _validationService = validationService;
    }

    [HttpPost("validate")]
    public ActionResult<Response> ValidateCard([FromBody] CreditCard card)
    {
        var response = _validationService.ValidateCard(card);

        if (response.IsSuccess)
        {
            return Ok(response);
            
        }
        return BadRequest(response);
    }
}

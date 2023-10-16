using DesafioNtt.Application.DTOs.Responses.AddressResponseDTO;
using DesafioNtt.Application.Interfaces.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DesafioNtt.Api.Controllers.v1.Address;

[ApiController]
[Route("v1/api/address")]
public class ControllerAddress : ControllerBase
{
    private readonly IUseCaseQuery<AddressResponse> _getAddress;
    
    public ControllerAddress(IUseCaseQuery<AddressResponse> getAddress)
    {
        _getAddress = getAddress;
    }
    
    [HttpGet]
    [Authorize]
    [Route("")]
    public async Task<IActionResult> GetAddress(int id)
    {
        var principal = HttpContext.User;
        var userId = principal?.Claims?.SingleOrDefault(p => p.Type.Contains("nameidentifier"))?.Value;
        if (userId == null)
            return Unauthorized();
        var addresses = await _getAddress.Execute(userId);
        return Ok(addresses);
    }
}
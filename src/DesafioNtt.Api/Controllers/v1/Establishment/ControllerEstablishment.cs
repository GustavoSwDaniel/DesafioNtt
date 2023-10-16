using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioNtt.Application.DTOs.Requests.EstablishmentRequestDTO;
using DesafioNtt.Application.DTOs.Responses.EstablishmentResponseDTO;
using DesafioNtt.Application.Interfaces.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioNtt.Api.Controllers.v1.Establishment
{
    [ApiController]
    [Route("v1/api/establishment")]
    public class ControllerEstablishment: ControllerBase
    {
        private readonly IUseCase<EstablishmentRegistration, EstablishmentRegistered> _establishmentRegistration;

        public ControllerEstablishment(
            IUseCase<EstablishmentRegistration, EstablishmentRegistered> establishmentRegistration)
        {
            _establishmentRegistration = establishmentRegistration;
        }

        [HttpPost]
        [Authorize]
        [Route("register")]
        public async Task<IActionResult> RegisterEstablishment(EstablishmentRegistration establishmentData)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var principal = HttpContext.User;
            var userId = principal?.Claims?.SingleOrDefault(p => p.Type.Contains("nameidentifier"))?.Value;
            if (userId == null)
                return Unauthorized();
            EstablishmentRegistered result = await _establishmentRegistration.Execute(establishmentData, userId);
            return Ok(result);
        }
    }
}
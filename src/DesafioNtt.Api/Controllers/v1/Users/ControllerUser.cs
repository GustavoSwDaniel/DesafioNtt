using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DesafioNtt.Application.DTOs.Requests.UserRequestDTO;
using DesafioNtt.Application.DTOs.Responses.UserReponseDTO;
using DesafioNtt.Identity.Interfaces.UseCase.Identity;

namespace DesafioNtt.Api.Controllers.v1.Users;

[ApiController]
[Route("v1/api/user")]
public class ControllerUser : ControllerBase
{

    private readonly IUserCaseIdentity<UserRegistration, UserRegistered> _UseCaseRegister;
    private readonly IUserCaseIdentity<UserAuthenticate, UserAuthenticated> _UseCaseAuth;

    public ControllerUser(IUserCaseIdentity<UserRegistration, UserRegistered> UseCaseRegister, IUserCaseIdentity<UserAuthenticate, UserAuthenticated> UseCaseAuth)
    {
        _UseCaseRegister = UseCaseRegister;
        _UseCaseAuth = UseCaseAuth;
    }

    [HttpPost(Name = "RegisterUser")]
    [Route("register")]
    public async Task<IActionResult> RegisterUser(UserRegistration userRegister)
    {   
        if(!ModelState.IsValid)
            return BadRequest();
        var result = await _UseCaseRegister.Execute(userRegister);
        return StatusCode(500);
    }

    [HttpPost(Name = "Login")]
    [Route("login")]
    public async Task<IActionResult> LoginUser(UserAuthenticate userData)
    {
        if(!ModelState.IsValid)
            return BadRequest();

        var result = await _UseCaseAuth.Execute(userData);
        return Ok(result);

    }

}

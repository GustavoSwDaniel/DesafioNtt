using System.Threading.Tasks;
using DesafioNtt.Application.Responses.DTOs;
using DesafioNtt.Application.Requests.DTOs;
using DesafioNtt.Application.Interfaces.UseCase.Identity;
using DesafioNtt.Identity.Configurations;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace DesafioNtt.Identity.useCase.Users
{
    public class UserRegister : IUserCaseIdentity<UserRegistration, UserRegistered>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public UserRegister(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, 
                            IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<UserRegistered> RegisterUser(UserRegistration userRegister)
        {
            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                await _userManager.SetLockoutEnabledAsync(user, false);
            }

            await _signInManager.SignInAsync(user, false);
            var userResponse = new UserRegistered(result.Succeeded);

            if (!result.Succeeded && result.Errors.Count() > 0)
            {
                userResponse.AddErrors(result.Errors.Select(e => e.Description));
            }

            return userResponse;
        }

        public async Task<UserRegistered> Execute(UserRegistration userRegister)
        {
            return await RegisterUser(userRegister);
        }
    }
}

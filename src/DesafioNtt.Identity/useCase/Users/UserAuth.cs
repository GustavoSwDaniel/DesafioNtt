using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using DesafioNtt.Application.Responses.DTOs;
using DesafioNtt.Application.Requests.DTOs;
using DesafioNtt.Application.Interfaces.UseCase.Identity;
using DesafioNtt.Identity.Configurations;

namespace DesafioNtt.Identity.useCase.Users
{
    public class UserAuth : IUserCaseIdentity<UserAuthenticate, UserAuthenticated>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public UserAuth(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        private string GenerateJwt(IEnumerable<Claim> claims, DateTime dateExpiration)
        {
            
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow, 
                expires: dateExpiration.ToUniversalTime(),
                signingCredentials: _jwtOptions.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        private async Task<UserAuthenticated> CredentialsGenerate(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var accessTokenClaims = await GetClaims(user, addClaimsUser: true);
            var refreshTokenClaims = await GetClaims(user, addClaimsUser: false);
            var dataExpiracaoAccessToken = DateTime.UtcNow.AddSeconds(_jwtOptions.AccessTokenExpiration);
            var dataExpiracaoRefreshToken = DateTime.UtcNow.AddSeconds(_jwtOptions.RefreshTokenExpiration);
            var accessToken = GenerateJwt(accessTokenClaims, dataExpiracaoAccessToken);
            var refreshToken = GenerateJwt(refreshTokenClaims, dataExpiracaoRefreshToken);

            return new UserAuthenticated
            (
                success: true,
                accessToken: accessToken,
                refreshToken: refreshToken
            );
        }

        private async Task<IList<Claim>> GetClaims(IdentityUser user, bool addClaimsUser)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            if (addClaimsUser)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                claims.AddRange(userClaims);

                foreach (var role in roles)
                    claims.Add(new Claim("role", role));
            }

            return claims;
        }

        private async Task<UserAuthenticated> UserAuthenticate(UserAuthenticate userAuthenticate)
        {
            var result = await _signInManager.PasswordSignInAsync(userAuthenticate.Email, userAuthenticate.Password, false, true);

            if (result.Succeeded)
            {
                return await CredentialsGenerate(userAuthenticate.Email);
            }

            var userAuthenticated = new UserAuthenticated();
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    userAuthenticated.AddError("Usuário bloqueado");
                else if (result.IsNotAllowed)
                    userAuthenticated.AddError("Essa conta não tem permissão para fazer login");
                else if (result.RequiresTwoFactor)
                    userAuthenticated.AddError("É necessário confirmar o login no seu segundo fator de autenticação");
                else
                    userAuthenticated.AddError("Usuário ou senha estão incorretos");
            }

            return userAuthenticated;
        }

        public async Task<UserAuthenticated> Execute(UserAuthenticate userAuthenticate)
        {
            return await UserAuthenticate(userAuthenticate);
        }
    }
}

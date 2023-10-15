using DesafioNtt.Infrastructure.Persistence;
using DesafioNtt.Identity.Persistence;
using DesafioNtt.Application.Interfaces.UseCase.Identity;
using DesafioNtt.Identity.useCase.Users;
using DesafioNtt.Application.Requests.DTOs;
using DesafioNtt.Application.Responses.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace DesafioNtt.Api.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            );
            
            services.AddDbContext<IdentityDataContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserCaseIdentity<UserRegistration, UserRegistered>, UserRegister>();
            services.AddScoped<IUserCaseIdentity<UserAuthenticate, UserAuthenticated>, UserAuth>();
        }
        
    }
}



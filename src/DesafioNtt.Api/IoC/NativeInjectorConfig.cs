using DesafioNtt.Infrastructure.Persistence;
using DesafioNtt.Identity.Persistence;
using AutoMapper;
using DesafioNtt.Identity.useCase.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DesafioNtt.Application.DTOs.Requests.UserRequestDTO;
using DesafioNtt.Application.DTOs.Responses.UserReponseDTO;
using DesafioNtt.Application.DTOs.Requests.EstablishmentRequestDTO;
using DesafioNtt.Application.DTOs.Responses.AddressResponseDTO;
using DesafioNtt.Application.DTOs.Responses.EstablishmentResponseDTO;
using DesafioNtt.Application.ExternalService;
using DesafioNtt.Application.Interfaces.IViaCepService;
using DesafioNtt.Application.Interfaces.UseCase;
using DesafioNtt.Application.Mappers;
using DesafioNtt.Application.UseCase.AddressUseCase;
using DesafioNtt.Application.UseCase.EstablishmentUseCases;
using DesafioNtt.Domain.Entities.Establishment;
using DesafioNtt.Domain.Interfaces.Repositories;
using DesafioNtt.Infrastructure.Persistence.Repository.EstablishmentRepo;
using DesafioNtt.Domain.Entities.Address;
using DesafioNtt.Domain.Entities.Phone;
using DesafioNtt.Identity.Interfaces.UseCase.Identity;


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
            services.AddScoped<IRepository<Establishment>, EstablishmentRepository>();
            services.AddScoped<IRepository<Address>, AddressRepository>();
            services.AddScoped<IRepository<Phone>, PhoneRepository>();
            services.AddScoped<IUseCase<EstablishmentRegistration, EstablishmentRegistered>, EstablishmentRegister>();
            services.AddScoped<IUseCaseQuery<AddressResponse>, GetAddress>();
            services.AddSingleton(configuration); 
            services.AddAutoMapper(typeof(GetAddress));
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddHttpClient<IViaCepService, ViaCepService>();
    
        }
    }
}


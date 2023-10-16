using DesafioNtt.Application.DTOs.Requests.EstablishmentRequestDTO;
using DesafioNtt.Application.DTOs.Responses.EstablishmentResponseDTO;
using DesafioNtt.Application.ExternalService;
using DesafioNtt.Application.Interfaces.IViaCepService;
using DesafioNtt.Domain.Entities.Establishment;
using DesafioNtt.Domain.Interfaces.Repositories;
using DesafioNtt.Application.Interfaces.UseCase;
using DesafioNtt.Domain.Entities.Address;
using DesafioNtt.Domain.Entities.Phone;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DesafioNtt.Application.UseCase.EstablishmentUseCases
{
    public class EstablishmentRegister : IUseCase<EstablishmentRegistration, EstablishmentRegistered>
    {
        private readonly IRepository<Establishment> _establishmentRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Phone> _phoneRepository;
        private readonly IViaCepService _viaCepService;

        public EstablishmentRegister(IRepository<Establishment> establishmentRepository,
                                     IRepository<Address> addressRepository,
                                     IRepository<Phone> phoneRepository,
                                     IViaCepService viaCepService)
        {
            _establishmentRepository = establishmentRepository;
            _addressRepository = addressRepository;
            _viaCepService = viaCepService;
            _phoneRepository = phoneRepository;
        }
        
        private Establishment BuildEstablishment(EstablishmentRegistration establishmentData, string? userId)
        {
            return new Establishment(establishmentData.Name,
                establishmentData.EstablishmentPhone,
                establishmentData.EstablishmentStartDate,
                userId
            );
        }

        private Address BuildAddress(ViaCepResponse? addressData, string number, string complement, int establishmentId)
        {
            return new Address(
                addressData?.Logradouro ?? string.Empty,
                addressData?.Bairro ?? string.Empty,
                number,
                complement,
                addressData?.Localidade ?? string.Empty,
                addressData?.Cep ?? string.Empty,
                addressData?.UF ?? string.Empty,
                establishmentId
            );
        }

        private Phone BuildPhone(string phone, int establishmentId)
        {
            string[] phoneParts = phone.Split('-');

            return new Phone(
                phoneParts[0],
                phoneParts[1],
                establishmentId
            );
        }

        private async Task<ViaCepResponse?> GetAddressByCep(string? cep)
        {
            if (cep == null)
                return null;
            var address = await _viaCepService.GetCep(cep);
            if (address == null)
                return null;
            return address;
        }

        public async Task<EstablishmentRegistered> Execute(EstablishmentRegistration establishmentData, string? userId)
        {
            try
            {
                ViaCepResponse? data = await GetAddressByCep(establishmentData.EstablishmentAddress?.Cep!);
                if (data == null)
                {
                    var establishmentResponse = new EstablishmentRegistered(false);
                    establishmentResponse.AddErrors(new[] { "Cep does not found" });
                    return await Task.FromResult(establishmentResponse);
                }

                Establishment establishment = BuildEstablishment(establishmentData, userId);
                bool response = await _establishmentRepository.Add(establishment);

                Address establishmentAddress = BuildAddress(data, establishmentData.EstablishmentAddress!.Number,
                    establishmentData.EstablishmentAddress!.Complement, establishment.Id);
                await _addressRepository.Add(establishmentAddress);

                Phone establishmentPhone = BuildPhone(establishmentData.EstablishmentPhone, establishment.Id);
                await _phoneRepository.Add(establishmentPhone);

                return await Task.FromResult(new EstablishmentRegistered(response));
            }
            catch (Exception)
            {
                return new EstablishmentRegistered(false);
            }
        }
    }
}

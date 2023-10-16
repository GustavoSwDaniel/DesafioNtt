using AutoMapper;
using DesafioNtt.Application.DTOs.Responses.AddressResponseDTO;
using DesafioNtt.Domain.Entities.Address;

namespace DesafioNtt.Application.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Address, AddressResponse>();
    }
}
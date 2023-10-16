
using AutoMapper;
using DesafioNtt.Application.DTOs.Responses.AddressResponseDTO;
using DesafioNtt.Application.Interfaces.UseCase;
using DesafioNtt.Domain.Entities.Address;
using DesafioNtt.Domain.Entities.Establishment;
using DesafioNtt.Domain.Interfaces.Repositories;

namespace DesafioNtt.Application.UseCase.AddressUseCase;

public class GetAddress : IUseCaseQuery<AddressResponse>
{   
    private readonly IRepository<Establishment> _establishmentRepository;
    private readonly IRepository<Address> _addressRepository;
    private readonly IMapper _mapper;


    public GetAddress(IRepository<Establishment> establishmentRepository,
        IRepository<Address> addressRepository, IMapper mapper)
    {
        _establishmentRepository = establishmentRepository;
        _addressRepository = addressRepository;
        _mapper = mapper;

    }
    
    public async Task<List<AddressResponse>?> Execute(string userId)
    {
        var establishment= await _establishmentRepository.GetUserIdAsync(userId);
        if (establishment == null)
        {
            return null;
        }
        var addresses = await _addressRepository.GetListByIdAsync(establishment.Id);
        var mappedAddresses = _mapper.Map<List<AddressResponse>>(addresses);

        return mappedAddresses;
    }
}
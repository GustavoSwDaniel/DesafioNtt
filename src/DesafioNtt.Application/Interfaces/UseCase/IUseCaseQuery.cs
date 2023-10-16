using DesafioNtt.Application.DTOs.Responses.AddressResponseDTO;

namespace DesafioNtt.Application.Interfaces.UseCase;

public interface IUseCaseQuery<TResult>
{
    Task<List<TResult>?> Execute(string userId);
}
using DesafioNtt.Application.ExternalService;

namespace DesafioNtt.Application.Interfaces.IViaCepService;

public interface IViaCepService
{
    Task<ViaCepResponse?> GetCep(string cep);
}
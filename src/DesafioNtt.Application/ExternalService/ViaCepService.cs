using DesafioNtt.Application.Interfaces.IViaCepService;
using Newtonsoft.Json;

namespace DesafioNtt.Application.ExternalService
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ViaCepResponse?> GetCep(string? cep)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ViaCepResponse? viaCepResponse = JsonConvert.DeserializeObject<ViaCepResponse>(content);
                    return viaCepResponse;
                }
                else
                {
                    // Handle the error here
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
    }
}
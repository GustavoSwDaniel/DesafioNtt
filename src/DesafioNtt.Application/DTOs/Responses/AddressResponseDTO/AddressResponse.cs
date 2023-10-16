namespace DesafioNtt.Application.DTOs.Responses.AddressResponseDTO;

public class AddressResponse
{
    public int Id { get; set; }
    public string StreetName { get; set; }
    public string Neighborhood { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string City { get; set; }

    public DateTime CreatedAt { get; set; }
    public string Cep { get; set; }
    public string Uf { get; set; }

    public int EstablishmentId { get; set; }
}
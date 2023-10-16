

namespace DesafioNtt.Domain.Entities.Establishment
{
    public class Establishment
    {
        public Establishment(string name, string phone, 
            DateTime establishmentStartDate, string? userId)
        {
            Name = name;
            Phone = phone;
            EstablishmentStartDate = establishmentStartDate;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime EstablishmentStartDate { get; set; }
        public string? EstablishmentThumbnail { get; set; }
        public string? EstablishmentPhoto { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedAt { get; set; } 
        public ICollection<Address.Address>? EstablishmentAddress { get; set; }
        public ICollection<Phone.Phone>? EstablishmentPhones { get; set; }
    }
}
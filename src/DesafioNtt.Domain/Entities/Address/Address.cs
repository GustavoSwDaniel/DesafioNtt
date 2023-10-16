using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioNtt.Domain.Entities.Establishment;

namespace DesafioNtt.Domain.Entities.Address
{
    public class Address
    {
        public Address(string streetName, string neighborhood, string number, string complement, string city, string cep, string uf, int establishmentId)
        {
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            Neighborhood = neighborhood ?? throw new ArgumentNullException(nameof(neighborhood));
            Number = number ?? throw new ArgumentNullException(nameof(number));
            Complement = complement ?? throw new ArgumentNullException(nameof(complement));
            City = city ?? throw new ArgumentNullException(nameof(city));
            CreatedAt = DateTime.UtcNow;
            Cep = cep ?? throw new ArgumentNullException(nameof(cep));
            Uf = uf ?? throw new ArgumentNullException(nameof(uf));
            EstablishmentId = establishmentId;
        }

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
        public Establishment.Establishment? Establishment { get; set; }
    }
}
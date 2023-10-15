using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioNtt.Domain.Entities.Establishment;

namespace DesafioNtt.Domain.Entities.Address
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public string Neighborhood { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Cep { get; set; }
        public string Uf { get; set; }

        public int EstablishmentId { get; set; }
        public Establishment.Establishment Establishment { get; set; }
    }
}
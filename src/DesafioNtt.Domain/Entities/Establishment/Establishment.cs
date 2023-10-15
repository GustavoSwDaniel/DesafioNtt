using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioNtt.Domain.Entities.Address;
using DesafioNtt.Domain.Entities.Phone;

namespace DesafioNtt.Domain.Entities.Establishment
{
    public class Establishment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime EstablishmentStartDate { get; set; }
        public string EstablishmentThumbnail { get; set; }
        public string EstablishmentPhoto { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Address.Address> EstablishmentAddress { get; set; }
        public ICollection<Phone.Phone> EstablishmentPhones { get; set; }
    }
}
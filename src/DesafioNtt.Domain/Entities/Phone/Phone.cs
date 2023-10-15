using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioNtt.Domain.Entities.Phone
{
    public class Phone
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Ddd { get; set; }
        public DateTime CreatedAt { get; set; }
        public int EstablishmentId { get; set; }
        public Establishment.Establishment Establishment { get; set; }

    }
}
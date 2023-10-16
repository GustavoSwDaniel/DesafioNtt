using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;



namespace DesafioNtt.Application.DTOs.Requests.EstablishmentRequestDTO
{
    public class EstablishmentRegistration
    {
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EstablishmentStartDate { get; set; }
        
        [RegularExpression(@"^\d{2}-\d{9}$", ErrorMessage = "O formato do telefone deve ser XX-XXXXXXXXX.")]
        public string EstablishmentPhone { get; set; }
        
        public EstablishmentAddress? EstablishmentAddress { get; set; }
    }
}
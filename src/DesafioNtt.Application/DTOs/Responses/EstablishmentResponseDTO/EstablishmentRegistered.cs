using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioNtt.Application.DTOs.Responses.EstablishmentResponseDTO
{
    public class EstablishmentRegistered
    {
        public bool Sucess { get; private set; }

        public List<string> Errors { get; private set; }
        
        public EstablishmentRegistered() =>
            Errors = new List<string>();

        public EstablishmentRegistered(bool sucess = true) : this() =>
            Sucess = sucess;
        
        public void AddErrors(IEnumerable<string> erros) =>
            Errors.AddRange(erros);
    }
}
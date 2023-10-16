using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioNtt.Application.DTOs.Responses.UserReponseDTO
{
    public class UserRegistered
    {
        public bool Sucess { get; private set; }

        public List<string> Errors { get; private set; }

        public UserRegistered() =>
            Errors = new List<string>();

        public UserRegistered(bool sucess = true) : this() =>
            Sucess = sucess;

        public void AddErrors(IEnumerable<string> erros) =>
            Errors.AddRange(erros);
    }
}
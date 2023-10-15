using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioNtt.Application.Responses.DTOs;
using DesafioNtt.Application.Requests.DTOs;

namespace DesafioNtt.Application.Interfaces.UseCase.Identity
{
    public interface IUserCaseIdentity<TInput, TResult>
    {
        public Task<TResult> Execute(TInput userData);
    }
}
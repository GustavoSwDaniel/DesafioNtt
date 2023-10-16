using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioNtt.Domain.Entities.Address;
using DesafioNtt.Domain.Entities.Establishment;

namespace DesafioNtt.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<bool> Add(TEntity entity);
        Task<TEntity?> GetUserIdAsync(string userId);
        Task<List<TEntity>> GetListByIdAsync(int establishmentId);
        
    }
}
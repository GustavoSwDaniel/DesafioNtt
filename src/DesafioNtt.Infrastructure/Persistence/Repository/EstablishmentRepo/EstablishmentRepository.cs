using DesafioNtt.Domain.Entities.Address;
using DesafioNtt.Domain.Entities.Establishment;
using DesafioNtt.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioNtt.Infrastructure.Persistence.Repository.EstablishmentRepo
{
    public class EstablishmentRepository : IRepository<Establishment>
    {
        private readonly ApplicationDbContext _context;

        public EstablishmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Establishment?> GetUserIdAsync(string userId)
        {
            return await _context.Establishment
                .Where(e => e.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public Task<List<Establishment>> GetListByIdAsync(int establishmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Add(Establishment entity)
        {
            try
            {   
                _context.Establishment.Add(entity);
                Console.WriteLine(entity);
                Console.WriteLine("UserRepository.Add");
                var result = await _context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
          
        }
    }
}
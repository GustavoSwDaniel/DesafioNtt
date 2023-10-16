using DesafioNtt.Domain.Entities.Address;
using DesafioNtt.Domain.Entities.Establishment;
using DesafioNtt.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioNtt.Infrastructure.Persistence.Repository.EstablishmentRepo;

public class AddressRepository : IRepository<Address>
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Address?> GetUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Address>> GetListByIdAsync(int establishmentId)
    {
        return await _context.Address
            .Where(a => a.EstablishmentId == establishmentId)
            .ToListAsync();
    }
    public async Task<bool> Add(Address entity)
    {
        try
        {   
            _context.Address.Add(entity);
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
using DesafioNtt.Domain.Entities.Address;
using DesafioNtt.Domain.Entities.Establishment;
using DesafioNtt.Domain.Entities.Phone;
using DesafioNtt.Domain.Interfaces.Repositories;

namespace DesafioNtt.Infrastructure.Persistence.Repository.EstablishmentRepo;

public class PhoneRepository : IRepository<Phone>
{
    private readonly ApplicationDbContext _context;

    public PhoneRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Add(Phone entity)
    {
        try
        {   
            _context.Phone.Add(entity);
            var result = await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public Task<Phone?> GetUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Phone>> GetListByIdAsync(int establishmentId)
    {
        throw new NotImplementedException();
    }
}
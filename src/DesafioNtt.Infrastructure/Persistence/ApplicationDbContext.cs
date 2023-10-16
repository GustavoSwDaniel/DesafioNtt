 
using Microsoft.EntityFrameworkCore;
using DesafioNtt.Domain.Entities.Address;
using DesafioNtt.Domain.Entities.Phone;
using DesafioNtt.Domain.Entities.Establishment;

namespace DesafioNtt.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Establishment> Establishment { get; set; }
    }
} 

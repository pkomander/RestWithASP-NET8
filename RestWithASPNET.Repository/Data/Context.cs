using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Domain;

namespace RestWithASPNET.Repository.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt)
        {
            
        }

        public DbSet<Person> Persons { get; set; }
    }
}

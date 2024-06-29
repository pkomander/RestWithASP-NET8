using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Model;

namespace RestWithASPNET.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt)
        {
            
        }

        public DbSet<Person> Persons { get; set; }
    }
}

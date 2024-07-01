using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Domain;
using RestWithASPNET.Domain.Identity;

namespace RestWithASPNET.Repository.Data
{
    public class Context : IdentityDbContext<User, Role, int,
                                            IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                            IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public Context(DbContextOptions<Context> opt) : base(opt)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });
        }
    }
}

using Domain.Entities.Idenetity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Presistence.Identity
{
    public class IdentityAppDbContext: IdentityDbContext 
    {
        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //Configure 7 DbSets
 
         modelBuilder.Entity<Address>().ToTable("Addresses");

        }
    }  
}

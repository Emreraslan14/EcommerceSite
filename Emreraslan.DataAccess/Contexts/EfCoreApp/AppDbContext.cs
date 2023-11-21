using Emreraslan.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Emreraslan.DataAccess.Contexts.EfCoreApp
{
    public class AppDbContext : IdentityDbContext<User,Role,string>
    {
        public AppDbContext(DbContextOptions options) : base (options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}

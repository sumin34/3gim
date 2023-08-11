using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using _3gim.Models;
using Microsoft.EntityFrameworkCore;

namespace _3gim.Data
{
    public class _3gimDbContext : IdentityDbContext<_3gimMember>
    {
        public _3gimDbContext(DbContextOptions<_3gimDbContext> option) : base(option)
        {
        }

        public DbSet<Product> Product { get; set; }

        public DbSet<Order> Order { get; set; }


        public DbSet<Warehousing> Warehousing { get; set; }

        public DbSet<Temperature> Temperature { get; set; }


    }
}

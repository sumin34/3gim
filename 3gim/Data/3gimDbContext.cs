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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Warehousing>()
                .Property(w => w.LotNumber)
                .HasDefaultValue(23000);

            modelBuilder.Entity<Warehousing>()
                .Property(w => w.Store)
                .HasDefaultValue(0);

            modelBuilder.Entity<Warehousing>()
               .Property(w => w.Release)
               .HasDefaultValue(0);

            modelBuilder.Entity<Warehousing>()
               .Property(w => w.Note)
               .HasDefaultValue("");


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Temperature> Temperature { get; set; }

        public DbSet<Defects> Defects { get; set; }
        public DbSet<Supplies> Supplies { get; set; }


    }
}

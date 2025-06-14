
using Microsoft.EntityFrameworkCore;
using OfficeApi.Models;

namespace OfficeApi.Data
{
    public class OfficeDbContext : DbContext
    {
        public OfficeDbContext(DbContextOptions<OfficeDbContext> options)
         : base(options)
        { }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Office>()
            .HasMany(o => o.Employees)
            .WithOne(e => e.Office)
            .HasForeignKey(e => e.OfficeCode)
            .IsRequired();

        }
    }

}
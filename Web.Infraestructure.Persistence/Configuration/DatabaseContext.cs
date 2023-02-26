using Microsoft.EntityFrameworkCore;
using Web.Domain.Entities;

namespace Web.Infraestructure.Persistence.Configuration
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receipt>().ToTable("Receipts");
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace PD_App2.Models
{
    public class InfografiaContext : DbContext
    {
        public InfografiaContext(DbContextOptions<InfografiaContext> options) : base(options) 
        {

        }

        public DbSet<Infografia> Infografias { get; set; }
        public DbSet<Character> characters { get; set; }
        public DbSet<Episode> episodes { get; set; }
        public DbSet<Quote> quotes { get; set; }
        public DbSet<Death> deaths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Infografia>().HasData(
            //    new Infografia { }
            //    );
        }
    }
}

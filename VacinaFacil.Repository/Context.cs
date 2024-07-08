using Microsoft.EntityFrameworkCore;
using VacinaFacil.Entity.Entities;

namespace VacinaFacil.Repository
{
    public class Context : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}

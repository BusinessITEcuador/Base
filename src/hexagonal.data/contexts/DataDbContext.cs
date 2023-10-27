using hexagonal.domain.entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace hexagonal.data.contexts
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> opts) : base(opts)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new PersonConfig());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<UsuarioEntity> Usuario { get; set; }

        public DbSet<GeneroEntity> Genero { get; set; }
    }
}
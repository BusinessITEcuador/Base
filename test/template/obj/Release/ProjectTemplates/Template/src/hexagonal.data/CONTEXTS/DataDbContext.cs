using $safeprojectname$.configs;
using hexagonal.domain.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.contexts
{
  public class DataDbContext : DbContext
  {
    public DataDbContext(DbContextOptions<DataDbContext> opts) : base(opts)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfiguration(new PersonConfig());
      
    }
    public DbSet<PersonEntity> AccountEntity { get; set; }
    

    public override int SaveChanges()
    {
      try
      {
        int response = base.SaveChanges();
        return response;
      }
      catch
      {
        this.ChangeTracker.Clear();
        throw;
      }
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      try
      {
        var response = base.SaveChangesAsync(cancellationToken);
        return response;
      }
      catch
      {
        throw;
      }
    }
  }
}

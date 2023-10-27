using hexagonal.data.contexts;
using hexagonal.data.repositories.generics;
using hexagonal.data.repositories.person;
using hexagonal.domain.repositories.interfaces;
using hexagonal.domain.repositories.interfaces.generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.services
{
  public class UnitOfWork: IUnitOfWork
  {
    protected readonly DataDbContext _context;

    public UnitOfWork(DataDbContext context)
    {
      _context = context;
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
      return new Repository<T>(_context);
    }

    public IPersonDomainRepository GetAccountRepository()
    {
      return new PersonRepository(_context);
    }

    public void Save()
    {
      _context.SaveChanges();
    }
    public void Dispose()
    {
      _context.Dispose();
    }

    public void CleanTracker()
    {
      this._context.ChangeTracker.Clear();
    }

    public void BeginTransaction()
    {
      _context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
      _context.Database.CommitTransaction();
    }

    public void RollbackTransaction()
    {
      _context.Database.RollbackTransaction();
    }

    public IPersonDomainRepository GetPersonRepository()
    {
      throw new NotImplementedException();
    }
  }
}

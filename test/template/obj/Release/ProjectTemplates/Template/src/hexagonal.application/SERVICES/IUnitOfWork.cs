using hexagonal.domain.repositories.interfaces;
using hexagonal.domain.repositories.interfaces.generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.services
{
  public interface IUnitOfWork
  {
    IRepository<T> GetRepository<T>() where T : class;
    IPersonDomainRepository GetPersonRepository();
    

    void Save();
    void CleanTracker();

    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
  }
}

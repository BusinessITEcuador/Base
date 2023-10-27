using $safeprojectname$.entities;
using $safeprojectname$.repositories.interfaces.generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.repositories.interfaces
{
  public interface IPersonDomainRepository : IRepository<PersonEntity>
  {
  }
}

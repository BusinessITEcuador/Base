using $safeprojectname$.contexts;
using $safeprojectname$.repositories.generics;
using hexagonal.domain.entities;
using hexagonal.domain.repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.repositories.person
{
  public class PersonRepository : Repository<PersonEntity>, IPersonDomainRepository
  {
    public PersonRepository(DataDbContext context) : base(context)
    {
    }
  }
}

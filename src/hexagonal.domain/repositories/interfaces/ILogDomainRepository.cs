using hexagonal.domain.entities;
using hexagonal.domain.repositories.interfaces.generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexagonal.domain.repositories.interfaces
{
    public interface ILogDomainRepository : IRepository<LogEntity>
    {
    }
}

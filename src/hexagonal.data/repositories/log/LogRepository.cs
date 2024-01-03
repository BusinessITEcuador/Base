using hexagonal.data.contexts;
using hexagonal.data.repositories.generics;
using hexagonal.domain.entities;
using hexagonal.domain.repositories.interfaces;

namespace hexagonal.infrastructure.data.repositories.log
{
    public class LogRepository : Repository<LogEntity>, ILogDomainRepository
    {
        public LogRepository(DataDbContext context) : base(context)
        {
        }
    }
}
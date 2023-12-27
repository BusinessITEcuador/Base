using hexagonal.data.contexts;
using hexagonal.data.repositories.generics;
using hexagonal.domain.entities;
using hexagonal.domain.repositories.interfaces;

namespace hexagonal.infrastructure.data.repositories.usuario
{
    public class CuentaRepository : Repository<CuentaEntity>, ICuentaDomainRepository
    {
        public CuentaRepository(DataDbContext context) : base(context)
        {
        }
    }
}
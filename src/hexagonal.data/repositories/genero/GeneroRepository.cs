using hexagonal.data.contexts;
using hexagonal.data.repositories.generics;
using hexagonal.domain.entities;
using hexagonal.domain.repositories.interfaces;

namespace hexagonal.infrastructure.data.repositories.genero
{
    public class GeneroRepository : Repository<GeneroEntity>, IGeneroDomainRepository
    {
        public GeneroRepository(DataDbContext context) : base(context)
        {
        }
    }
}
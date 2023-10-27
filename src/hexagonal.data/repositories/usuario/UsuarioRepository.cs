using hexagonal.data.contexts;
using hexagonal.data.repositories.generics;
using hexagonal.domain.entities;
using hexagonal.domain.repositories.interfaces;

namespace hexagonal.infrastructure.data.repositories.usuario
{
    public class UsuarioRepository : Repository<UsuarioEntity>, IUsuarioDomainRepository
    {
        public UsuarioRepository(DataDbContext context) : base(context)
        {
        }
    }
}
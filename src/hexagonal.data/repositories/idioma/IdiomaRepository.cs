using hexagonal.data.contexts;
using hexagonal.data.repositories.generics;
using hexagonal.domain.entities;
using hexagonal.domain.repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexagonal.infrastructure.data.repositories.idioma
{
    public class IdiomaRepository : Repository<IdiomaEntity>, IIdiomaDomainRepository
    {
        public IdiomaRepository(DataDbContext context) : base(context)
        {
        }
    }
}

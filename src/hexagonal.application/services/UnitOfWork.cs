using hexagonal.data.contexts;
using hexagonal.data.repositories.generics;
using hexagonal.domain.repositories.interfaces;
using hexagonal.domain.repositories.interfaces.generics;
using hexagonal.infrastructure.data.repositories.genero;
using hexagonal.infrastructure.data.repositories.idioma;
using hexagonal.infrastructure.data.repositories.usuario;

namespace hexagonal.application.services
{
    public class UnitOfWork : IUnitOfWork
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

        public IUsuarioDomainRepository GetUsuarioRepository()
        {
            return new UsuarioRepository(_context);
        }

        public IGeneroDomainRepository GetGeneroRepository()
        {
            return new GeneroRepository(_context);
        }

        public IIdiomaDomainRepository GetIdiomaRepository()
        {
            return new IdiomaRepository(_context);
        }

        public void SaveSync()
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
    }
}
using hexagonal.data.contexts;
using hexagonal.data.exceptions;
using hexagonal.domain.models;
using hexagonal.domain.repositories.interfaces.generics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace hexagonal.data.repositories.generics
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public DataDbContext _context { get; set; }

        public Repository(DataDbContext contex)
        {
            _context = contex;
        }

        public virtual async Task<PagedCollection<T>> GetPagin(int offset, int limit, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var count = await dbQuery.CountAsync();
            var items = await dbQuery
              .Skip(offset)
              .Take(limit)
              .ToArrayAsync();
            return new PagedCollection<T>()
            {
                Items = items,
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual IList<TV> GetAllWhereCustomSync<TV>(Expression<Func<T, TV>> expression, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var noreturn = dbQuery.Where(@where).AsEnumerable().Select(expression.Compile()).ToList();
            return noreturn;
        }

        public virtual async Task<PagedCollection<TV>> GetPaginCustom<TV>(Expression<Func<T, TV>> expression, int offset, int limit, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var count = await dbQuery.CountAsync();
            var items = await dbQuery
              .Skip(offset)
              .Take(limit)
              .ToArrayAsync();
            var data = items.Select(expression.Compile()).ToArray();
            return new PagedCollection<TV>()
            {
                Items = data,
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual async Task<PagedCollection<TV>> GetPaginCustomWhereOrder<TV>(Expression<Func<T, TV>> expression, Expression<Func<T, bool>> where, int offset, int limit, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var count = await dbQuery.CountAsync(where);

            dbQuery = dbQuery.Where(where).AsQueryable();

            if (orderBy != null)
                dbQuery = orderBy(dbQuery);

            var items = await dbQuery
              .Skip(offset)
              .Take(limit)
              .ToArrayAsync();
            var data = items.Select(expression.Compile()).ToArray();
            return new PagedCollection<TV>()
            {
                Items = data,
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual PagedCollection<TV> GetPaginCustomWhereOrderSync<TV>(Expression<Func<T, TV>> expression, Expression<Func<T, bool>> where, int offset, int limit, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var count = dbQuery.Count(where);

            dbQuery = dbQuery.Where(where).AsQueryable();

            if (orderBy != null)
                dbQuery = orderBy(dbQuery);

            var items = dbQuery
              .Skip(offset)
              .Take(limit)
              .ToArray();
            var data = items.Select(expression.Compile()).ToArray();
            return new PagedCollection<TV>()
            {
                Items = data,
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual async Task<PagedCollection<T>> GetPaginWhere(Expression<Func<T, bool>> where, int offset, int limit, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var count = await Task.FromResult(dbQuery.Count(where));

            var items = await Task.FromResult(dbQuery.Where(where).Skip(offset)
              .Take(limit));
            return new PagedCollection<T>()
            {
                Items = items.ToArray(),
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual async Task<PagedCollection<T>> GetPaginWhereOrder(Expression<Func<T, bool>> where, int offset, int limit, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            var count = await Task.FromResult(dbQuery.Count(where));
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }

            dbQuery = dbQuery.Where(where).AsQueryable();

            if (orderBy != null)
                dbQuery = orderBy(dbQuery);

            var dbResult = await Task.FromResult(dbQuery
              .Skip(offset)
              .Take(limit));
            return new PagedCollection<T>()
            {
                Items = dbResult.ToArray(),
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual PagedCollection<T> GetPaginWhereOrderSync(Expression<Func<T, bool>> where, int offset, int limit, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            var count = dbQuery.Count(where);
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }

            dbQuery = dbQuery.Where(where).AsQueryable();

            if (orderBy != null)
                dbQuery = orderBy(dbQuery);

            var dbResult = dbQuery
              .Skip(offset)
              .Take(limit);
            return new PagedCollection<T>()
            {
                Items = dbResult.ToArray(),
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual PagedCollection<T> GetPaginWhereSync(Expression<Func<T, bool>> where, int offset, int limit, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var count = dbQuery.Count(where);
            var items = dbQuery.Where(where).Skip(offset)
              .Take(limit);
            return new PagedCollection<T>()
            {
                Items = items.ToArray(),
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual async Task<IList<T>> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var retorno = await dbQuery.ToArrayAsync();
            return retorno;
        }

        public virtual IList<T> GetAllSync(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var retorno = dbQuery.ToList();
            return retorno;
        }

        public virtual IList<T> GetAllWhereSync(Expression<Func<T, bool>> where)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            var retorno = dbQuery.Where(where).ToList();
            return retorno;
        }

        public virtual async Task<IList<T>> GetAllOrder(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            if (orderBy != null)
                dbQuery = orderBy(dbQuery);
            var retorno = await dbQuery.ToArrayAsync();
            return retorno;
        }

        public virtual async Task<IList<T>> GetAllWhere(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var retorno = await dbQuery.Where(where).ToListAsync();
            return retorno;
        }

        public IList<T> GetAllWhereSync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var retorno = dbQuery.Where(where).ToList();
            return retorno;
        }

        public virtual async Task<IList<T>> GetAllWhereOrder(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            if (orderBy != null)
                dbQuery = orderBy(dbQuery);
            var retorno = await dbQuery.Where(where).ToListAsync();
            return retorno;
        }

        public virtual int Count(Expression<Func<T, bool>> predicates)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            return dbQuery.Count(predicates); ;
        }

        public virtual async Task<IList<T>> GetAllWhereOrderBy(Expression<Func<T, bool>> where, string[] parameterOrder, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            if (parameterOrder != null)
            {
                dbQuery = dbQuery.Where(where).AsQueryable();
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                IOrderedQueryable<T> query = null;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                if (!string.IsNullOrEmpty(parameterOrder[0]))
                {
                    var propertyInfo = typeof(T).GetProperty(parameterOrder[0]);
                    query = dbQuery.OrderBy(x => propertyInfo.GetValue(x, null));
                    for (int i = 1; i < parameterOrder.Length; i++)
                    {
                        var property = typeof(T).GetProperty(parameterOrder[i]);
                        query = query.ThenBy(x => property.GetValue(x, null));
                    }
                }
                var result = await Task.FromResult(query.ToList());
                return result;
            }
            var retorno = await dbQuery.Where(where).ToListAsync();
            return retorno;
        }

        public virtual async Task<T> GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var retorno = await dbQuery.FirstOrDefaultAsync(where);
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return retorno;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public virtual T FirstOrDefaultSync(Expression<Func<T, bool>> where)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            T retorno = dbQuery.FirstOrDefault(where);
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            return retorno;
        }

        public virtual T FirstOrDefaultSync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (Expression<Func<T, object>> navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            T result = dbQuery.FirstOrDefault(where);
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            return result;
        }

        public virtual T SingleSync(Expression<Func<T, bool>> where)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            var result = dbQuery.FirstOrDefault(where);
            if (result == null)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public virtual T SingleSync(Expression<Func<T, bool>> where, string entityMessage)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            var result = dbQuery.FirstOrDefault(where);
            if (result == null)
            {
                throw new NotFoundException(entityMessage);
            }
            return result;
        }

        public virtual T SingleSync(Expression<Func<T, bool>> where, string entityMessage, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (Expression<Func<T, object>> navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var result = dbQuery.FirstOrDefault(where);
            if (result == null)
            {
                throw new NotFoundException(entityMessage);
            }
            return result;
        }

        public virtual void AddRangeSync(ICollection<T> items)
        {
            _context.AddRange(items);
        }

        public virtual void RemoveRangeSync(ICollection<T> items)
        {
            _context.RemoveRange(items);
        }

        public void AddSync(params T[] items)
        {
            foreach (T item in items)
            {
                _context.Entry(item).State = EntityState.Added;
            }
        }

        public virtual async Task UpdateAsync(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
        }

        public virtual void UpdateSync(ICollection<T> items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
        }

        public virtual void UpdateSync(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
        }

        public virtual void UpdateRangeSync(ICollection<T> items)
        {
            _context.UpdateRange(items);
        }

        public virtual async Task RemoveAsync(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
        }

        public virtual void RemoveSync(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
        }
    }
}
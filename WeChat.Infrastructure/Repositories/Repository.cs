using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeChat.Domain.SeedWork;

namespace WeChat.Infrastructure
{
    public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        private readonly EFContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public Repository(EFContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        public T Add(T entity) => _context.Set<T>().Add(entity);

        public Task<T> GetAsync(object guid) => _context.Set<T>().FindAsync(guid);

        public IEnumerable<T> ListEntities() => _context.Set<T>();
    }
}

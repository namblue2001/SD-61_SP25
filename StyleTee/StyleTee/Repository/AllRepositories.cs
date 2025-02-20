using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using System.Linq.Expressions;

namespace StyleTee.Repository
{
    public class AllRepositories<T> : IAllRepositories<T> where T : class
    { 
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public AllRepositories(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public T FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

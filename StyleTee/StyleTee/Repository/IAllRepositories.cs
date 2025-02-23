using System.Linq.Expressions;

namespace StyleTee.Repository
{
    public interface IAllRepositories<T> where T : class
    {
        void Insert (T entity);
        void Save();
        T FindBy(Expression<Func<T, bool>> predicate);
    }
}

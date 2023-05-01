using System.Linq.Expressions;

namespace Users.DataAccess.Repositories;

public interface IRepositoryBase<T>
{
    Task Create(T entity);

    void Update(T entity);

    Task<T> FindByCondition(Expression<Func<T, bool>> expression);
}

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Users.DataAccess.Context;

namespace Users.DataAccess.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext RepositoryContext { get; set; }

    public RepositoryBase(RepositoryContext repositoryContext)
    {
        this.RepositoryContext = repositoryContext;
    }

    public async Task<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return await RepositoryContext.Set<T>().FirstOrDefaultAsync(expression);
    }

    public async Task Create(T entity) => await RepositoryContext.Set<T>().AddAsync(entity);

    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
}

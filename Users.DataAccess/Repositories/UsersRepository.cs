using Users.DataAccess.Context;
using Users.DataAccess.Entities;

namespace Users.DataAccess.Repositories;

public class UsersRepository : RepositoryBase<User>, IUsersRepository
{
    public UsersRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}

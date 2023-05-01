using Users.DataAccess.Context;
using Users.DataAccess.Entities;

namespace Users.DataAccess.Repositories;

public class UserAuthorizationsRepository : RepositoryBase<UserAuthorization>, IUserAuthorizationsRepository
{
    public UserAuthorizationsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}

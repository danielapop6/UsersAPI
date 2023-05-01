using Users.DataAccess.Context;

namespace Users.DataAccess.Repositories;

public class UserAuthorizationsRepository : RepositoryBase<UserAuthorizationsRepository>, IUserAuthorizationsRepository
{
    public UserAuthorizationsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}

using Microsoft.EntityFrameworkCore;
using Users.DataAccess.Entities;

namespace Users.DataAccess.Context;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {
    }

    DbSet<User> Users { get; set; }

    DbSet<UserAuthorization> UsersAuthorizations { get; set; }
}

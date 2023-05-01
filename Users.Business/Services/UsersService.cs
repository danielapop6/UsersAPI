using Users.Business.Enums;
using Users.Business.Logic;
using Users.DataAccess.Context;
using Users.DataAccess.Entities;
using Users.DataAccess.Repositories;

namespace Users.Business.Services;

public class UsersService : IUsersService
{
    private RepositoryContext _repositoryContext;
    private IUsersRepository _usersRepository;
    private IUserAuthorizationsRepository _userAuthorizationsRepository;

    public UsersService(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public IUsersRepository Users
    {
        get
        {
            if (_usersRepository == null)
            {
                _usersRepository = new UsersRepository(_repositoryContext);
            }

            return _usersRepository;
        }
    }
    public IUserAuthorizationsRepository UserAuthorizations
    {
        get
        {
            if (_userAuthorizationsRepository == null)
            {
                _userAuthorizationsRepository = new UserAuthorizationsRepository(_repositoryContext);
            }
            return _userAuthorizationsRepository;
        }
    }

    public async Task Save()
    {
        await _repositoryContext.SaveChangesAsync();
    }

    public async Task<string> GenerateOTPForUser(Guid userId)
    {
        var user = _usersRepository.FindByCondition(user => user.Id == userId);

        if (user == null)
        {
            return string.Empty;
        }

        string authorizationCode = UserAuthorizationLogic.GenerateAuthorizationCode();

        return authorizationCode;
    }

    public async Task SaveUserAuthorizationCode(Guid userId, string code)
    {
        var user = _usersRepository.FindByCondition(user => user.Id == userId);

        if (user == null)
        {
            return;
        }

        var authorization = await _userAuthorizationsRepository.FindByCondition(userAuthorization => userAuthorization.UserId == userId);

        if (authorization == null)
        {
            var userAuthorization = new UserAuthorization()
            {
                AuthorizationCode = code,
                UserId = userId,
                InsertDateTime = DateTime.Now,
                ExpiryDateTime = DateTime.Now.AddSeconds(Constants.OTPExpirationTimeInSeconds),
                Status = (int)AuthorizationCodeStatus.UNUSED
            };

            await _userAuthorizationsRepository.Create(userAuthorization);
        }
        else
        {
            authorization.AuthorizationCode = code;
            authorization.InsertDateTime = DateTime.Now;
            authorization.ExpiryDateTime = DateTime.Now.AddSeconds(Constants.OTPExpirationTimeInSeconds);
            authorization.Status = (int)AuthorizationCodeStatus.UNUSED;

            _userAuthorizationsRepository.Update(authorization);
        }

        await Save();
    }

    public async Task<bool> ValidateOTPForUser(Guid userId, string code)
    {
        var authorization = await _userAuthorizationsRepository.FindByCondition(userAuthorization => userAuthorization.UserId == userId);

        if (authorization == null)
        {
            return false;
        }

        if (!authorization.AuthorizationCode.Equals(code))
        {
            return false;
        }

        if(DateTime.Compare(authorization.ExpiryDateTime, DateTime.Now) < 0)
        {
            return false;
        }

        if(authorization.Status == (int)AuthorizationCodeStatus.USED)
        {
            return false;
        }

        return true;
    }

    public async Task MarkOTPAsUsed(Guid userId)
    {
        var authorization = await _userAuthorizationsRepository.FindByCondition(userAuthorization => userAuthorization.UserId == userId);

        if (authorization == null)
        {
            return;
        }

        authorization.Status = (int)AuthorizationCodeStatus.USED;

        _userAuthorizationsRepository.Update(authorization);

        await Save();
    }
}

namespace Users.Business.Services;

public interface IUsersService
{
    Task<string> GenerateOTPForUser(Guid userId);

    Task MarkOTPAsUsed(Guid userId);

    Task SaveUserAuthorizationCode(Guid userId, string code);

    bool ValidateOTPForUser(Guid userId, string authorizationCode);
}

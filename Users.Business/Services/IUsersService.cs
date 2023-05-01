﻿namespace Users.Business.Services;

public interface IUsersService
{
    Task<string> GenerateOTPForUser(Guid userId);

    Task SaveUserAuthorizationCode(Guid userId, string code);
    Task<bool> ValidateOTPForUser(Guid userId);
}

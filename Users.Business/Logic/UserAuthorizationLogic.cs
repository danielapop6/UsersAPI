namespace Users.Business.Logic;

public static class UserAuthorizationLogic
{
    private static readonly Random _random = new Random();

    public static string GenerateAuthorizationCode()
    {
        return _random.Next(Constants.OTPMaximumRandomValue).ToString(Constants.OTPStringFormat);
    }
}

using Microsoft.AspNetCore.Mvc;
using Users.Business.Services;
using UsersAPI.Models;
using UsersAPI.Requests;

namespace UsersAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("GenerateOTPForUser")]
    public async Task<string> GenerateOTPForUser(OTPRequest request)
    {
        try
        {
            if(request is null)
            {
                return "Empty body";
            }


            string otp = await _usersService.GenerateOTPForUser(request.UserId);
            if(string.IsNullOrEmpty(otp))
            {
                return "Something when wrong";
            }

            await _usersService.SaveUserAuthorizationCode(request.UserId, otp);

            return otp;
        }
        catch (Exception)
        {
            return "Exception occured, please try again";
        }
    }

    [HttpPost("ValidateOTPForUser")]
    public async Task<bool> ValidateOTPForUser(OTPValidationRequest request)
    {
        try
        {
            if (request is null)
            {
                return false;
            }


            bool isValidCode = _usersService.ValidateOTPForUser(request.UserId, request.AuthorizationCode);
            if (isValidCode)
            {
                await _usersService.MarkOTPAsUsed(request.UserId);
            }

            return isValidCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

namespace UsersAPI.Requests
{
    public class OTPValidationRequest
    {
        public Guid UserId { get; set; }

        public string AuthorizationCode { get; set; }
    }
}

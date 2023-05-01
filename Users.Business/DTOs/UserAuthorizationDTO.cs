using Users.Business.Enums;

namespace Users.Business.DTOs
{
    public class UserAuthorizationDTO
    {
        public Guid UserId { get; set; }

        public string AuthorizationCode { get; set; }

        public DateTime InsertDateTime { get; set; }

        public DateTime ExpiryDateTime { get; set; }

        public AuthorizationCodeStatus Status { get; set; }
    }
}

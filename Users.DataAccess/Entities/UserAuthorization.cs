using System.ComponentModel.DataAnnotations.Schema;

namespace Users.DataAccess.Entities;

public class UserAuthorization
{
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public User? User { get; set; } 

    public string AuthorizationCode { get; set; } 

    public DateTime InsertDateTime { get; set; }

    public DateTime ExpiryDateTime { get; set; }

    public int Status { get; set; }
}

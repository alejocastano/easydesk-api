using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyDesk.Domain;

public class UserUserRole
{
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public int UserRoleId { get; set; }
    [ForeignKey("UserRoleId")]
    public UserRole UserRole { get; set; }
}
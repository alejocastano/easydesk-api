using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyDesk.Domain;

public class UserRole
{
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public int RoleId { get; set; }
    [ForeignKey("RoleId")]
    public Role Role { get; set; }
}
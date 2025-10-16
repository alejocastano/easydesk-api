using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyDesk.Domain;

public class User : BaseModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<Ticket> CreatedTickets { get; set; } = new List<Ticket>();
    public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
    public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
}
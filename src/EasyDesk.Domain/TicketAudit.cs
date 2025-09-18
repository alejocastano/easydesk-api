using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyDesk.Domain;

public class TicketAudit : BaseModel
{
    [Required]
    public int TicketId { get; set; }
    [ForeignKey("TicketId")]
    public Ticket Ticket { get; set; }

    public int PerformedByUserId { get; set; }
    [ForeignKey("PerformedByUserId")]
    public User PerformedByUser { get; set; }

    [Required]
    public string ActionType { get; set; } // created, updated, commented, approved, rejected, etc

    public string? Description { get; set; }
}
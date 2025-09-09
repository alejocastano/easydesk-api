using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyDesk.Domain;

public class Ticket : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    public int CreatedByUserId { get; set; }
    [ForeignKey("CreatedByUserId")]
    public User CreatedByUser { get; set; }

    public int? AssignedToUserId { get; set; }
    [ForeignKey("AssignedToUserId")]
    public User? AssignedToUser { get; set; }

    public int StatusId { get; set; }
    [ForeignKey("StatusId")]
    public TicketStatus Status { get; set; }

    public int PriorityId { get; set; }
    [ForeignKey("PriorityId")]
    public TicketPriority Priority { get; set; }

    public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
    public ICollection<TicketAudit> Audits { get; set; } = new List<TicketAudit>();
    
}

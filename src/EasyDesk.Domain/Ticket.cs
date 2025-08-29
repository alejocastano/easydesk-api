using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyDesk.Domain;

public class Ticket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

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

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdate { get; set; }

    public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();

    public Ticket(Guid id, string title, string description, DateTime createdAt, DateTime? closedAt = null)
    {
        Id = id;
        Title = title;
        Description = description;
        CreatedAt = createdAt;
        ClosedAt = closedAt;
    }
}

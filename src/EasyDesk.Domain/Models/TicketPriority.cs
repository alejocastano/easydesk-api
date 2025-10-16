using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyDesk.Domain;

public class TicketPriority : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
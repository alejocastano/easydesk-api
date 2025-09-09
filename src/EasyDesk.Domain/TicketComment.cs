using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyDesk.Domain;

public class TicketComment : BaseModel
{
    public int TicketId { get; set; }
    [ForeignKey("TicketId")]
    public Ticket Ticket { get; set; }

    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public string Content { get; set; }

}
using EasyDesk.Domain;

namespace EasyDesk.Application;

public class TicketDTO {
    public string Title { get; set; }
    public string Description { get; set; }
    public TicketPriorityLevel PriorityId { get; set; } = TicketPriorityLevel.Medium;
}

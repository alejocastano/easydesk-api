namespace EasyDesk.Application;

public class TicketDTO {
    public string Title { get; set; }
    public string Description { get; set; }
    public int PriorityId { get; set; } = 2; // Default to Medium
}

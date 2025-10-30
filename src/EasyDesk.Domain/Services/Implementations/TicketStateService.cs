namespace EasyDesk.Domain;

public class TicketStateService : ITicketStateService
{
    private readonly Dictionary<TicketStatusType, List<TicketStatusType>> _transitions = new()
    {
        { TicketStatusType.Open, new List<TicketStatusType> { TicketStatusType.InProgress, TicketStatusType.Resolved } },
        { TicketStatusType.InProgress, new List<TicketStatusType> { TicketStatusType.Open, TicketStatusType.Resolved } },
        { TicketStatusType.Resolved, new List<TicketStatusType> { TicketStatusType.Closed, TicketStatusType.InProgress } },
        { TicketStatusType.Closed, new List<TicketStatusType> { TicketStatusType.Reopened } },
        { TicketStatusType.Reopened, new List<TicketStatusType> { TicketStatusType.InProgress, TicketStatusType.Resolved } }
    };

    public bool CanTransition(TicketStatusType from, TicketStatusType to)
    {
        if (_transitions.TryGetValue(from, out var possibleTransitions))
        {
            return possibleTransitions.Contains(to);
        }
        
        return false;
    }
}
    
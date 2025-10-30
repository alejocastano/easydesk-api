namespace EasyDesk.Domain;

public interface ITicketStateService
{
    bool CanTransition(TicketStatusType from, TicketStatusType to);
}
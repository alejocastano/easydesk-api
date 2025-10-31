namespace EasyDesk.Domain;

public interface ITicketStateService
{
    bool CanTransition(TicketStatusType from, TicketStatusType to);
    bool CanUserTransition(TicketStatusType from, TicketStatusType to, User user);
}
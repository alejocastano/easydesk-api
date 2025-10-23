using EasyDesk.Domain;

namespace EasyDesk.Application;

public interface ITicketStateService
{
    bool CanTransition(TicketStatus from, TicketStatus to);
}
using EasyDesk.Domain;

namespace EasyDesk.Application;

public interface ITicketStateService
{
    bool CanTransition(TicketStatusType from, TicketStatusType to);
}
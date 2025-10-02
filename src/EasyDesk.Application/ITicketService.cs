using EasyDesk.Domain;

namespace EasyDesk.Application;

public interface ITicketService
{
    Task<Ticket> CreateTicketAsync(TicketDTO ticketDto, int createdByUserId);
    Task<Ticket> GetTicketByIdAsync(string id);
    Task<IEnumerable<Ticket>> GetAllTicketsAsync();
}
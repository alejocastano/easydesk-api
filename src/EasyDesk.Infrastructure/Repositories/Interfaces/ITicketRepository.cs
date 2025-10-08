using EasyDesk.Domain;

namespace EasyDesk.Infrastructure;

public interface ITicketRepository
{
    Task<Ticket> AddAsync(Ticket ticket);
    Task<Ticket> GetByIdAsync(string id);
    Task<Ticket> GetOpenTicketByUserAndTitleAsync(int userId, string title);
    Task<IEnumerable<Ticket>> GetAllAsync();
}
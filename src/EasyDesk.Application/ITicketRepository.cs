using EasyDesk.Domain;

namespace EasyDesk.Application;

public interface ITicketRepository
{
    Task<Ticket> AddAsync(Ticket ticket);
    Task<Ticket> GetByIdAsync(int id);
    Task<IEnumerable<Ticket>> GetAllAsync();
}
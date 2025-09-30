using EasyDesk.Domain;

namespace EasyDesk.Application;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<Ticket> CreateTicketAsync(TicketDTO ticketDto, int createdByUserId)
    {
        // Validate if there is not an open ticket with the same title (Case insensitive)   
        var allTickets = await _ticketRepository.GetAllAsync();
        var existingTicket = allTickets.FirstOrDefault(t => t.Title.Equals(ticketDto.Title, StringComparison.OrdinalIgnoreCase) && t.StatusId == 1);

        if (existingTicket != null)
        {
            throw new InvalidOperationException("An open ticket with the same title already exists.");
        }

        if (DateTime.UtcNow.Hour < 15 && DateTime.UtcNow.Hour >= 3)
        {
            throw new InvalidOperationException("Tickets can only be created between 3PM UTC and 3AM UTC.");
        }

        var ticket = new Ticket
        {
            Title = ticketDto.Title,
            Description = ticketDto.Description,
            PriorityId = ticketDto.PriorityId,
            CreatedByUserId = createdByUserId,
            StatusId = 1
        };

        await _ticketRepository.AddAsync(ticket);
        return ticket;
    }

    public async Task<Ticket> GetTicketByIdAsync(int id)
    {
        return await _ticketRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        return await _ticketRepository.GetAllAsync();
    }
}
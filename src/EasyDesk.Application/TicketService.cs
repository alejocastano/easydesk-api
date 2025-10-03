using EasyDesk.Domain;

namespace EasyDesk.Application;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ITicketIdGenerator _ticketIdGenerator;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<Ticket> CreateTicketAsync(TicketDTO ticketDto, int createdByUserId)
    {
        if(ticketDto.Title.Length < 10 || ticketDto.Title.Length > 100)
        {
            throw new ArgumentException("Title must be between 10 and 100 characters.");
        }

        if(ticketDto.Description.Length < 20 || ticketDto.Description.Length > 1000)
        {
            throw new ArgumentException("Description must be between 20 and 1000 characters.");
        }
        
        var allTickets = await _ticketRepository.GetAllAsync();
        var existingTicket = allTickets.FirstOrDefault(t => t.Title.Equals(ticketDto.Title, StringComparison.OrdinalIgnoreCase) && t.StatusId == 1);

        if (existingTicket != null)
        {
            throw new InvalidOperationException("An open ticket with the same title already exists.");
        }

        var pacificZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
        var pacificNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, pacificZone);

        if (pacificNow.DayOfWeek < DayOfWeek.Monday || pacificNow.DayOfWeek > DayOfWeek.Friday)
        {
            throw new InvalidOperationException("Tickets can only be created Monday to Friday PST.");
        }

        if (pacificNow.Hour < 8 || pacificNow.Hour >= 18)
        {
            throw new InvalidOperationException("Tickets can only be created between 8AM PST and 6PM PST.");
        }

        var newId = await _ticketIdGenerator.GenerateIdAsync();

        var ticket = new Ticket
        {
            Id = newId,
            Title = ticketDto.Title,
            Description = ticketDto.Description,
            PriorityId = ticketDto.PriorityId,
            CreatedByUserId = createdByUserId,
            StatusId = 1
        };

        await _ticketRepository.AddAsync(ticket);
        return ticket;
    }

    public async Task<Ticket> GetTicketByIdAsync(string id)
    {
        return await _ticketRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        return await _ticketRepository.GetAllAsync();
    }
}
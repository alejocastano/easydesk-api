using EasyDesk.Domain;
using Microsoft.EntityFrameworkCore;

namespace EasyDesk.Infrastructure;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket> AddAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<Ticket> GetByIdAsync(string id)
    {
        return await _context.Tickets
            .Include(t => t.Status)
            .Include(t => t.Priority)
            .Include(t => t.CreatedByUser)
            .Include(t => t.AssignedToUser)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Ticket> GetOpenTicketByUserAndTitleAsync(int userId, string title)
    {
        return await _context.Tickets
            .Include(t => t.Status)
            .FirstOrDefaultAsync(t =>
                t.CreatedByUserId == userId &&
                t.Title.ToLower() == title.ToLower() &&
                t.StatusId == (int)TicketStatusType.Open);
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        return await _context.Tickets
            .Include(t => t.Status)
            .Include(t => t.Priority)
            .Include(t => t.CreatedByUser)
            .Include(t => t.AssignedToUser)
            .ToListAsync();
    }
}
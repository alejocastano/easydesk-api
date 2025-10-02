using EasyDesk.Domain;
using EasyDesk.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EasyDesk.Application;

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

    public async Task<Ticket> GetByIdAsync(int id)
    {
        return await _context.Tickets
            .Include(t => t.Status)
            .Include(t => t.Priority)
            .Include(t => t.CreatedByUser)
            .Include(t => t.AssignedToUser)
            .FirstOrDefaultAsync(t => t.Id == id);
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
using EasyDesk.Domain;
using EasyDesk.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EasyDesk.Application;

public class TicketIdGenerator : ITicketIdGenerator
{
    private readonly AppDbContext _context;

    public TicketIdGenerator(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateIdAsync()
    {
        var result = await _context.Database
            .SqlQueryRaw<string>("SELECT generate_new_ticket_id()")
            .SingleAsync();

        return result;
    }
}
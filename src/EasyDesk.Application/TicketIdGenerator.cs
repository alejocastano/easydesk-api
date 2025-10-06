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
        var result = await _context.Set<TicketIdResult>()
            .FromSqlRaw("SELECT generate_new_ticket_id() AS \"Value\"")
            .FirstAsync();

        return result.Value;
    }
}

public class TicketIdResult
{
    public string Value { get; set; } = null!;
}
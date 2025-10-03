using EasyDesk.Domain;
using EasyDesk.Infrastructure;

namespace EasyDesk.Application;

public TicketIdGenerator : ITicketIdGenerator
{
    private readonly AppDbContext _context;

    public TicketIdGenerator(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateNextIdAsync()
    {
        var result = await _context.Database
            .SqlQuery<string>($"SELECT generate_new_ticket_id()")
            .SingleAsync();

        return result;
    }
}
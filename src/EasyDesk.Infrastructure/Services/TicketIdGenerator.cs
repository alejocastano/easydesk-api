using EasyDesk.Domain;
using EasyDesk.Application;
using Microsoft.EntityFrameworkCore;

namespace EasyDesk.Infrastructure;

public class TicketIdGenerator : ITicketIdGenerator
{
    private readonly AppDbContext _context;

    public TicketIdGenerator(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateIdAsync()
    {
        var todayPrefix = "TKT-" + DateTime.UtcNow.ToString("yyyyMMdd") + "-";

        var count = await _context.Tickets
            .CountAsync(t => t.Id.StartsWith(todayPrefix));

        var nextSeq = count + 1;

        var newId = $"{todayPrefix}{nextSeq.ToString().PadLeft(3, '0')}";
        return newId;
    }
}
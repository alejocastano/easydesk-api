using EasyDesk.Infrastructure;
using EasyDesk.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EasyDesk.Application;

[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{
    private readonly AppDbContext _context;

    public TicketController(AppDbContext context)
    {
        _context = context;
    }
}
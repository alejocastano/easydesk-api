using EasyDesk.Infrastructure;
using EasyDesk.Application;
using Microsoft.AspNetCore.Mvc;

namespace EasyDesk.Api;

[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] TicketDTO ticketDto, [FromHeader] int createdByUserId)
    {
        try{
            var ticket = await _ticketService.CreateTicketAsync(ticketDto, createdByUserId);
            return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketById(string id)
    {
        var ticket = await _ticketService.GetTicketByIdAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }
}
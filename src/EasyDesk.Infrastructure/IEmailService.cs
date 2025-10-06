using EasyDesk.Domain;

namespace EasyDesk.Infrastructure;

public interface IEmailService
{
    Task SendTicketConfirmAsync(User user, Ticket ticket);
}
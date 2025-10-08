using EasyDesk.Domain;

namespace EasyDesk.Application;

public interface IEmailService
{
    Task SendTicketConfirmAsync(User user, Ticket ticket);
}
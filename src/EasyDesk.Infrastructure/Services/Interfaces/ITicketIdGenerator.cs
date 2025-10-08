namespace EasyDesk.Infrastructure;

public interface ITicketIdGenerator
{
    Task<string> GenerateIdAsync();
}
namespace EasyDesk.Application;

public interface ITicketIdGenerator
{
    Task<string> GenerateIdAsync();
}
namespace EasyDesk.Domain;

public interface ITicketValidationService
{
    Task ValidateBusinessHoursAsync();
}
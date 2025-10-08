namespace EasyDesk.Domain;

public class TicketValidationService : ITicketValidationService
{

    public async Task ValidateBusinessHoursAsync()
    {
        var pacificZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
        var pacificNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, pacificZone);

        if (pacificNow.DayOfWeek < DayOfWeek.Monday || pacificNow.DayOfWeek > DayOfWeek.Friday)
            throw new InvalidOperationException("Tickets can only be created Monday to Friday PST.");

        if (pacificNow.Hour < 8 || pacificNow.Hour >= 18)
            throw new InvalidOperationException("Tickets can only be created between 8AM PST and 6PM PST.");
    }
}
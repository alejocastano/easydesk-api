using FluentValidation;

namespace EasyDesk.Application;

public class TicketValidator: AbstractValidator<TicketDTO>
{
    public TicketValidator()
    {
        RuleFor(x => x.Title).Length(10, 100);
        RuleFor(x => x.Description).Length(20, 1000);
    }
}
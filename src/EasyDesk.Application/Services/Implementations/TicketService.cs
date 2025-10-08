using EasyDesk.Domain;
using EasyDesk.Infrastructure;
using FluentValidation;

namespace EasyDesk.Application;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITicketIdGenerator _ticketIdGenerator;
    private readonly ITicketValidationService _ticketValidationService;
    private readonly IEmailService _emailService;

    public TicketService(
        ITicketRepository ticketRepository,
        IUserRepository userRepository,
        ITicketIdGenerator ticketIdGenerator,
        IEmailService emailService,
        ITicketValidationService ticketValidationService)
    {
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _ticketIdGenerator = ticketIdGenerator;
        _emailService = emailService;
        _ticketValidationService = ticketValidationService;
    }

    public async Task<Ticket> CreateTicketAsync(TicketDTO ticketDto, int createdByUserId)
    {
        TicketValidator validator = new TicketValidator();

        validator.ValidateAndThrow(ticketDto);

        var createdByUser = await _userRepository.GetByIdAsync(createdByUserId);
        if (createdByUser == null)
            throw new InvalidOperationException("The user creating the ticket does not exist.");

        var existingTicket = await _ticketRepository.GetOpenTicketByUserAndTitleAsync(createdByUserId, ticketDto.Title);
        if (existingTicket != null)
            throw new InvalidOperationException("An open ticket with the same title already exists for this user.");
    
        await _ticketValidationService.ValidateBusinessHoursAsync();

        var newId = await _ticketIdGenerator.GenerateIdAsync();

        var ticket = new Ticket
        {
            Id = newId,
            Title = ticketDto.Title,
            Description = ticketDto.Description,
            PriorityId = (int) ticketDto.PriorityId,
            CreatedByUserId = createdByUserId,
            StatusId = (int) TicketStatusType.Open,
        };

        await _ticketRepository.AddAsync(ticket);

        await _emailService.SendTicketConfirmAsync(createdByUser, ticket);

        return ticket;
    }

    public async Task<Ticket> GetTicketByIdAsync(string id)
    {
        return await _ticketRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        return await _ticketRepository.GetAllAsync();
    }
}
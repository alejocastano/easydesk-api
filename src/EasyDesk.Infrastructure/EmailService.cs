using EasyDesk.Domain;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace EasyDesk.Infrastructure;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public EmailService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _configuration = configuration;
    }

    public async Task SendTicketConfirmAsync(User user, Ticket ticket)
    {
        var apiKey = _configuration["MailerSend:ApiKey"];
        var fromEmail = _configuration["MailerSend:FromEmail"];
        var fromName = _configuration["MailerSend:FromName"];

        var requestBody = new
        {
            from = new
            {
                email = fromEmail,
                name = fromName
            },
            to = new[]
            {
                new
                {
                    email = user.Email,
                    name = user.Name
                }
            },
            subject = "Your Ticket Confirmation",
            html = $"<p>Dear {user.Name},</p><p>Your ticket with ID {ticket.Id} has been created successfully.</p><p>Thank you for using our service!</p>"
        };

        var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var response = await _httpClient.PostAsync("https://api.mailersend.com/v1/email", jsonContent);

        if(!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to send email: {error}");
        }
    }
}
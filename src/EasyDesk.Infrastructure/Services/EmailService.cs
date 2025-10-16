using EasyDesk.Domain;
using EasyDesk.Application;
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
        var apiUrl = _configuration["MailerSend:ApiUrl"];
        var apiKey = _configuration["MailerSend:ApiKey"];
        var fromEmail = _configuration["MailerSend:FromEmail"];
        var fromName = _configuration["MailerSend:FromName"];
        var template = await File.ReadAllTextAsync(_configuration["MailerSend:TemplateRoute"]);

        template = template.Replace("{{UserName}}", user.Name)
                           .Replace("{{TicketId}}", ticket.Id);

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
            html = template
        };

        var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var response = await _httpClient.PostAsync(apiUrl, jsonContent);

        if(!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to send email: {error}");
        }
    }
}
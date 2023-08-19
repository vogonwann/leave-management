using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HR.LeaveManagement.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        EmailSettings = emailSettings.Value;
    }

    public EmailSettings EmailSettings { get; }
    
    public async Task<bool> SendEmail(EmailMessage email)
    {
        var client = new SendGridClient(EmailSettings.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress { Email = EmailSettings.FromAddress, Name = EmailSettings.FromName };
        
        var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
        var response = await client.SendEmailAsync(message); 
        
        return response.StatusCode is System.Net.HttpStatusCode.Accepted or System.Net.HttpStatusCode.OK;
    }
}
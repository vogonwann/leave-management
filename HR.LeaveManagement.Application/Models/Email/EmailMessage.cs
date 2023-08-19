namespace HR.LeaveManagement.Application.Models.Email;

public class EmailMessage
{
    public string To { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string From { get; set; } = default!;
    public string Body { get; set; }
}
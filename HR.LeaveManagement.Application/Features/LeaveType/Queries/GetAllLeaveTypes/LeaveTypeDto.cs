namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class LeaveTypeDto
{
    public int Id { get; set; }   
    public string Name { get; set; } = default!;
    public int DefaultDays { get; set; }
}
using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain;

public class LeaveRequest : BaseEntity
{
    public LeaveType? LeaveType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DateRequested { get; set; }
    public bool? Approved { get; set; }
    public bool Canceled { get; set; }
    public string? RequestComments { get; set; }
    public string RequestingEmployeeId { get; set; } = default!;
}
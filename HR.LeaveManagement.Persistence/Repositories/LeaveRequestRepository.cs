using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext hrDatabaseContext) : base(hrDatabaseContext)
    {
    }

    public async Task<LeaveRequest?> GetLeaveRequestWithDetails(int id)
    {
        return await _context.LeaveRequests
            .Include(lr => lr.LeaveType)
            .FirstOrDefaultAsync(lr => lr.Id == id);
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        return await _context.LeaveRequests
            .Include(r => r.LeaveType)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        return await _context.LeaveRequests
            .Where(lr => lr.RequestingEmployeeId == userId)
            .Include(lr => lr.LeaveType)
            .ToListAsync();
    }
}
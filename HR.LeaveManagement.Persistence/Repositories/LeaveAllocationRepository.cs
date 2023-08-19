using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext hrDatabaseContext) : base(hrDatabaseContext)
    {
    }

    public async Task<LeaveAllocation> GetLeaveAllocationsWithDetails(int id)
    {
        var leaveAllocation = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(la => la.Id == id);
        return leaveAllocation;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        var leaveAllocations = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        var leaveAllocations = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .Where(la => la.EmployeeId == userId)
            .ToListAsync();
        return leaveAllocations;
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations
            .AnyAsync(la => la.EmployeeId == userId 
                            && la.LeaveTypeId == leaveTypeId 
                            && la.Period == period);
    }

    public async Task AddAllocations(List<LeaveAllocation> leaveAllocations)
    {
        await _context.AddRangeAsync(leaveAllocations);
        await _context.SaveChangesAsync();
    }

    public async Task<LeaveAllocation> GetUsersAllocation(string userId, int leaveTypeId)
    {
        return (await _context.LeaveAllocations
            .FirstOrDefaultAsync(al => al.EmployeeId == userId && al.LeaveTypeId == leaveTypeId))!;
    }
}
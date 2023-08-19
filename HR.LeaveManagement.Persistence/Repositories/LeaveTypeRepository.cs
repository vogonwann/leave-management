using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{

    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDatabaseContext hrDatabaseContext) : base(hrDatabaseContext)
        {
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            return await _context.LeaveTypes.AnyAsync(lt => lt.Name.Equals(name));
        }

        public async Task<bool> IsLeaveTypeIdUnique(int id)
        {
            return await _context.LeaveTypes.AnyAsync(lt => lt.Id == id);
        }
    }
}
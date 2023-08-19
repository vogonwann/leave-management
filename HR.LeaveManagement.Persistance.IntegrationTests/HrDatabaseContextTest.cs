using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistance.IntegrationTests;

public class HrDatabaseContextTest
{
    private readonly HrDatabaseContext _hrDatabaseContext;

    public HrDatabaseContextTest()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _hrDatabaseContext = new HrDatabaseContext(dbOptions);
    }

    [Fact]
    public async void Save_SetDataCreatedValue()
    {
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();
        
        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async void Save_SetDataModifiedValue()
    {
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();
        
        leaveType.DateCreated.ShouldNotBeNull();
    }
    
}
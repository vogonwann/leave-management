using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypeMockLeaveTypeRepository()
    {
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            },
            new LeaveType
            {
                Id = 2,
                DefaultDays = 15,
                Name = "Test Sick"
            },
            new LeaveType
            {
                Id = 3,
                DefaultDays = 15,
                Name = "Test Maternity"
            },
        };

        var mockRepo = new Mock<ILeaveTypeRepository>();
        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(leaveTypes);

        mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });
        return mockRepo;
    }
}
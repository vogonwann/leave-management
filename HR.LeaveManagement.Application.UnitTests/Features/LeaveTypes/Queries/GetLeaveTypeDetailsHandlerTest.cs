using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypeDetailsHandlerTest
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly IMapper _mapper;

    public GetLeaveTypeDetailsHandlerTest()
    {
        _mockRepo = MockLeaveTypeRepository.GetLeaveTypeMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });
        
        _mapper = mapperConfig.CreateMapper();
    }
    
    [Fact]
    public async Task GetLeaveTypeDetailsTest()
    {
        var handler = new GetLeaveDetailsHandler(_mapper, _mockRepo.Object);
        
        var result = await handler.Handle(new GetLeaveTypeDetailsQuery(2), CancellationToken.None);

        result.ShouldBeOfType<LeaveTypeDetailsDto>();
        result.Name.ShouldBe("Test Vacation");
    }
}
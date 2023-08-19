using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public GetLeaveRequestDetailsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
        var result = _mapper.Map<LeaveRequestDetailsDto>(leaveAllocation);
        return result;
    }
}
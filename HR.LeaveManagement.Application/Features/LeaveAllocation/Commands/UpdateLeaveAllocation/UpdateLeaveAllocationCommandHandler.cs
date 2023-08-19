using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveAllocationCommandHandler(
        IMapper mapper,
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Allocation Request", validationResult);
        
        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);
        
        if (leaveAllocation is null)
            throw new NotFoundException(nameof(Domain.LeaveAllocation), request.Id);

        _mapper.Map(request, leaveAllocation);
        await _leaveAllocationRepository.UpdateAsync(leaveAllocation);
        return Unit.Value;
    }   
}
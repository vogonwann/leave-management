using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the object to delete
        var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);
        
        if (leaveTypeToDelete == null)
        {
            throw new NotFoundException(nameof(Domain.LeaveType), request.Id);
        }
        
        // Delete the object
        await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);
        
        // Return Unit.Value
        return Unit.Value;
    }
}
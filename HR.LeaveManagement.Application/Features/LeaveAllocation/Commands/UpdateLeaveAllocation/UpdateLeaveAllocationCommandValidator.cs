using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;

        RuleFor(p => p.NumberOfDays)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
        
        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be greater than or equal to current year.");

        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveAllocationMustExist)
            .WithMessage("{PropertyName} must be present.");
    }

    private async Task<bool> LeaveAllocationMustExist(int arg1, CancellationToken arg2)
    {
        var leaveAllocation = await _leaveTypeRepository.GetByIdAsync(arg1);
        return leaveAllocation != null;
    }

    private async Task<bool> LeaveTypeMustExist(int arg1, CancellationToken arg2)
    {
        var leaveType = await _leaveAllocationRepository.GetByIdAsync(arg1);
        return leaveType != null;
    }
}
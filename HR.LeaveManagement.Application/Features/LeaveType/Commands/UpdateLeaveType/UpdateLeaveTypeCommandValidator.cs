using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        RuleFor(p => p.Id)
            .MustAsync(LeaveTypeMustExist)
            .NotNull();
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must not exceed 70 characters.");

        RuleFor(p => p.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} cannot exceed 100.")
            .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1.");

        RuleFor(command => command)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("LeaveType already exists");
    }

    private Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
    {
        return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
    
    private Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        return _leaveTypeRepository.IsLeaveTypeIdUnique(id);
    }
}
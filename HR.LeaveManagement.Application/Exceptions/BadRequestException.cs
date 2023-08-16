using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = new List<string>();
        
        validationResult.Errors
            .ForEach(error => ValidationErrors.Add(error.ErrorMessage));
    }

    public List<string> ValidationErrors { get; set; }
}
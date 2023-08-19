using System.Net;
using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Exceptions;

namespace HR.LeaveManagement.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        dynamic problem;

        switch (exception)
        {   
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails
                {
                    Title = exception.Message,
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Detail = exception.InnerException?.Message
                };
                break;
            default:
                problem = new CustomProblemDetails
                {
                    Title = exception.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = exception.StackTrace
                };
                break;
        }
        
        context.Response.StatusCode = (int)statusCode;
        await HttpResponseJsonExtensions.WriteAsJsonAsync(context.Response, problem);
    }
}
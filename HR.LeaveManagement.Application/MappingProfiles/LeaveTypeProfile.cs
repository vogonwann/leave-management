using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        CreateMap<LeaveType, LeaveTypeDetailsDto>();
    }    
}
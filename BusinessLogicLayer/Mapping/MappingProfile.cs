using AutoMapper;
using BusinessLogicLayer.DTOs.Request;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RequestCreateEmployeeDto, Employee>();
            CreateMap<Employee, RequestUpdateEmployeeDto>();
        }
    }
}

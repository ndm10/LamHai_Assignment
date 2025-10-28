using BusinessLogicLayer.DTOs.Request;
using BusinessLogicLayer.DTOs.Response;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(RequestCreateEmployeeDto employee, string manageBy);
        Task DeleteEmployeeAsync(Guid id);
        Task<Employee?> GetEmployeeByIdAsync(Guid id);
        Task<ResponseGetEmployeesDto> GetEmployeesAsync(int? pageIndex, int? pageSize, bool? isActive, string? textSearch, string userId);
        Task<bool> IsCodeExist(string employeeCode, string userId, Guid? oldEmployeeId = null);
        Task<bool> IsEmailExist(string employeeEmail, string userId, Guid? oldEmployeeId = null);
        Task<bool> IsPhoneExist(string employeePhone, string userId, Guid? oldEmployeeId = null);
        Task UpdateEmployeeAsync(RequestUpdateEmployeeDto employee);
    }
}

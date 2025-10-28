using AutoMapper;
using BusinessLogicLayer.DTOs.Request;
using BusinessLogicLayer.DTOs.Response;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Shared;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWork;
using LinqKit;
using System.Runtime.Intrinsics.X86;

namespace BusinessLogicLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddEmployeeAsync(RequestCreateEmployeeDto employee, string manageBy)
        {
            // Mapping dto to entity
            var newEmployee = _mapper.Map<Employee>(employee);
            newEmployee.ManageById = manageBy;

            // Add to db
            await _unitOfWork.EmployeeRepository!.AddAsync(newEmployee);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            //Find employee by Id
            var employee = await _unitOfWork.EmployeeRepository!.GetByIdAsync(id);

            if (employee == null)
            {
                return;
            }

            // Soft delete
            employee.IsDeleted = true;
            _unitOfWork.EmployeeRepository!.Update(employee);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
        {
            return await _unitOfWork.EmployeeRepository!.GetByIdAsync(id);
        }

        public async Task<ResponseGetEmployeesDto> GetEmployeesAsync(int? pageIndex, int? pageSize, bool? isActive, string? textSearch, string userId)
        {
            // Initial default value
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Create predicate to filter employee
            var predicate = PredicateBuilder.New<Employee>(x => x.ManageById == userId);

            if (isActive.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == isActive);
            }

            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => x.EmployeeCode.Contains(textSearch) || x.FullName.Contains(textSearch) || x.Email.Contains(textSearch) || x.Phone.Contains(textSearch) || x.Address.Contains(textSearch));
            }

            // Get employees
            var employees = await _unitOfWork.EmployeeRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate);
            var totalRecords = await _unitOfWork.EmployeeRepository!.CountEntitiesAsync(predicate);

            // return response
            return new ResponseGetEmployeesDto
            {
                Employees = employees,
                PageIndex = pageIndexValue,
                PageSize = pageSizeValue,
                TotalCount = totalRecords,
                TextSearch = textSearch,
            };
        }

        public async Task<bool> IsCodeExist(string employeeCode, string userId, Guid? oldEmployeeId = null)
        {
            var predicate = PredicateBuilder.New<Employee>(x => x.EmployeeCode == employeeCode && x.ManageById == userId);
            if (oldEmployeeId != null)
            {
                predicate = predicate.And(x => x.Id != oldEmployeeId);
            }

            var employeeExist = await _unitOfWork.EmployeeRepository!.GetEntityByPredicateAsync(predicate);
            return employeeExist != null;
        }

        public async Task<bool> IsEmailExist(string employeeEmail, string userId, Guid? oldEmployeeId = null)
        {
            var predicate = PredicateBuilder.New<Employee>(x => x.Email == employeeEmail && x.ManageById == userId);
            if (oldEmployeeId != null)
            {
                predicate = predicate.And(x => x.Id != oldEmployeeId);
            }

            var employeeExist = await _unitOfWork.EmployeeRepository!.GetEntityByPredicateAsync(predicate);
            return employeeExist != null;
        }

        public async Task<bool> IsPhoneExist(string employeePhone, string userId, Guid? oldEmployeeId = null)
        {
            var predicate = PredicateBuilder.New<Employee>(x => x.Phone == employeePhone && x.ManageById == userId);
            if (oldEmployeeId != null)
            {
                predicate = predicate.And(x => x.Id != oldEmployeeId);
            }

            var employeeExist = await _unitOfWork.EmployeeRepository!.GetEntityByPredicateAsync(predicate);
            return employeeExist != null;
        }

        public async Task UpdateEmployeeAsync(RequestUpdateEmployeeDto employee)
        {
            // Find employee by Id
            var employeeUpdate = await _unitOfWork.EmployeeRepository!.GetByIdAsync(employee.Id);

            // If employee is not null, update employee
            if (employeeUpdate != null)
            {
                employeeUpdate.EmployeeCode = employee.EmployeeCode ?? employeeUpdate.EmployeeCode;
                employeeUpdate.FullName = employee.FullName ?? employeeUpdate.FullName;
                employeeUpdate.DateOfBirth = employeeUpdate.DateOfBirth;
                employeeUpdate.Email = employee.Email ?? employeeUpdate.Email;
                employeeUpdate.Phone = employee.Phone ?? employeeUpdate.Phone;
                employeeUpdate.Address = employee.Address ?? employeeUpdate.Address;
                employeeUpdate.IsActive = employee.IsActive;

                _unitOfWork.EmployeeRepository.Update(employeeUpdate);
                await _unitOfWork.SaveChangesAsync();
            }

            return;
        }
    }
}

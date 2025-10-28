using AutoMapper;
using BusinessLogicLayer.DTOs.Request;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? pageIndex, int? pageSize, bool? isActive, string? textSearch)
        {
            // Get current id of the login user
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var response = await _employeeService.GetEmployeesAsync(pageIndex, pageSize, isActive, textSearch, userId);

            ViewBag.PageIndex = response.PageIndex;
            ViewBag.PageSize = response.PageSize;
            ViewBag.TotalCount = response.TotalCount;
            ViewBag.TextSearch = response.TextSearch;

            return View(response.Employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestCreateEmployeeDto employee)
        {
            // Get current id of the login user
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            // Server-side validation: prevent future dates
            if (employee.DateOfBirth > DateTime.Today)
            {
                ModelState.AddModelError(nameof(employee.DateOfBirth), "Ngày sinh phải ở quá khứ.");
                return View(employee);
            }

            if (await _employeeService.IsCodeExist(employee.EmployeeCode, userId))
            {
                ModelState.AddModelError(nameof(employee.EmployeeCode), "Mã nhân viên đã tồn tại vui lòng thử lại");
                return View(employee);
            }
            if (await _employeeService.IsEmailExist(employee.Email, userId))
            {
                ModelState.AddModelError(nameof(employee.Email), "Email đã tồn tại vui lòng thử lại");
                return View(employee);
            }

            if (await _employeeService.IsPhoneExist(employee.Phone, userId))
            {
                ModelState.AddModelError(nameof(employee.Phone), "Số điện thoại đã tồn tại vui lòng thử lại");
                return View(employee);
            }

            await _employeeService.AddEmployeeAsync(employee, userId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var requestUpdateEmployeeDto = _mapper.Map<RequestUpdateEmployeeDto>(employee);
            return View(requestUpdateEmployeeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RequestUpdateEmployeeDto employee)
        {
            // Get current id of the login user
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Server-side validation: prevent future dates
                    if (employee.DateOfBirth > DateTime.Today)
                    {
                        ModelState.AddModelError(nameof(employee.DateOfBirth), "Ngày sinh phải ở quá khứ.");
                        return View(employee);
                    }

                    // Check input EmployeeCode
                    if (await _employeeService.IsCodeExist(employee.EmployeeCode, userId, employee.Id))
                    {
                        ModelState.AddModelError(nameof(employee.EmployeeCode), "Mã nhân viên đã tồn tại vui lòng thử lại");
                        return View(employee);
                    }

                    // Check input Email
                    if (await _employeeService.IsEmailExist(employee.Email, userId, employee.Id))
                    {
                        ModelState.AddModelError(nameof(employee.Email), "Email đã tồn tại vui lòng thử lại");
                        return View(employee);
                    }

                    // Check input Phone
                    if (await _employeeService.IsPhoneExist(employee.Phone, userId, employee.Id))
                    {
                        ModelState.AddModelError(nameof(employee.Phone), "Số điện thoại đã tồn tại vui lòng thử lại");
                        return View(employee);
                    }

                    await _employeeService.UpdateEmployeeAsync(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error updating employee.");
                }
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

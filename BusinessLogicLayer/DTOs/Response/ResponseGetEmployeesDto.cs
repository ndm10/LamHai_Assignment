using DataAccessLayer.Entities;

namespace BusinessLogicLayer.DTOs.Response
{
    public class ResponseGetEmployeesDto
    {
        public List<Employee>? Employees { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? TotalCount { get; set; }
        public string? TextSearch { get; set; }
    }
}

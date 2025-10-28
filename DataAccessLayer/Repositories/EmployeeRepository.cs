using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(LamHaiAssignmentDbContext context) : base(context)
        {
        }
    }
}

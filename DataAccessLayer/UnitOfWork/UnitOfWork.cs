using DataAccessLayer.Contexts;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LamHaiAssignmentDbContext _context;
        private bool _disposed = false;

        private IEmployeeRepository _EmployeeRepository = null!;

        public UnitOfWork(LamHaiAssignmentDbContext context)
        {
            _context = context;
        }

        public IEmployeeRepository? EmployeeRepository {
            get
            {
                return _EmployeeRepository ??= new EmployeeRepository(_context);
            }
        }
        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}

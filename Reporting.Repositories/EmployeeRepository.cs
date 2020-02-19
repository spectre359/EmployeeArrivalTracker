using Reporting.Data.Context;
using Reporting.Data.Entities;
using Reporting.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ReportingToolContext _context;

        public EmployeeRepository(ReportingToolContext context)
        {
            _context = context;
        }

        public async Task AddMany(List<Employee> employees)
        {
            await _context.AddRangeAsync(employees);
        }

        public Task<IQueryable<Employee>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

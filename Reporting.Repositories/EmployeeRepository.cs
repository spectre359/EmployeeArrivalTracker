using Reporting.Contracts.Employee;
using Reporting.Data.Context;
using Reporting.Data.Entities;
using Reporting.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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

        public Task<bool> AddMany(List<Employee> employees)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

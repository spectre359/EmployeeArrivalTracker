using Reporting.Data.Context;
using Reporting.Data.Entities;
using Reporting.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reporting.Contracts.Misc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

        public async Task<IQueryable<Employee>> GetAll(SearchFilter filter)
        {
            var qry = _context.Employees.Where(c => filter.EmployeeIds.Any(e => e == c.Id));

            if (!string.IsNullOrEmpty(filter.Name))
            {
                if (Regex.IsMatch(filter.Name, @"^\w+\s\w+$"))
                {
                    var splittedName = filter.Name.Split(' ');
                    qry = qry.Where(c => c.Name.ToLowerInvariant().Equals(splittedName[0].ToLowerInvariant()) && c.SurName.ToLowerInvariant().Equals(splittedName[1].ToLowerInvariant()));
                }
                else
                {
                    qry = qry.Where(c => c.Name.ToLowerInvariant().Contains(filter.Name.ToLowerInvariant()) || c.SurName.ToLowerInvariant().Equals(filter.Name.ToLowerInvariant()));
                }

            }
            if (!string.IsNullOrEmpty(filter.JobPosition))
            {
                qry = qry.Where(c => c.Role.ToLowerInvariant().Contains(filter.JobPosition.ToLowerInvariant()));
            }

            return qry;
        }
    }
}

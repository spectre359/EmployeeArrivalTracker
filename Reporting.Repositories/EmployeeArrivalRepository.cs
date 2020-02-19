using Reporting.Data.Context;
using Reporting.Data.Entities;
using Reporting.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Repositories
{
    public class EmployeeArrivalRepository : IEmployeeArrivalRepository
    {
        private readonly ReportingToolContext _context;

        public EmployeeArrivalRepository(ReportingToolContext context)
        {
            _context = context;
        }

        public async Task AddMany(List<EmployeeArrival> arrivals)
        {
            await _context.AddRangeAsync(arrivals);
        }

        public async Task<IQueryable<EmployeeArrival>> GetAll()
        {
           return  _context.EmployeeArrivals;
        }
    }
}
